using System.Security.Claims;
using System.Security.Cryptography;
using EventsAPI.Application.Authentication.Contracts;
using EventsAPI.Application.Authentication.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventsAPI.Infrastructure.Authentication;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly JwtSettings _settings;
    private readonly PasswordHasher<User> _passwordHasher = new();
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext dbContext,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<JwtSettings> options,
        ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
        _settings = options.Value;
        _logger = logger;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var exists = await _dbContext.Users.AnyAsync(user => user.Email.ToLower() == normalizedEmail, cancellationToken);
        if (exists)
        {
            return AuthResult.Failed("EmailAlreadyExists");
        }

        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = normalizedEmail,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            IsEmailConfirmed = false,
            EmailConfirmationToken = GenerateSecureToken(48)
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return await CreateAuthResultAsync(user, cancellationToken);
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
        if (user is null)
        {
            return AuthResult.Failed("InvalidCredentials");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return AuthResult.Failed("InvalidCredentials");
        }

        return await CreateAuthResultAsync(user, cancellationToken);
    }

    public async Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return AuthResult.Failed("InvalidRefreshToken");
        }

        refreshToken.RevokedAt = DateTime.UtcNow;
        var user = refreshToken.User;

        var newRefreshToken = CreateRefreshToken(user.Id);
        _dbContext.RefreshTokens.Add(newRefreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAt) = _jwtTokenGenerator.GenerateToken(user, Array.Empty<Claim>());
        var response = new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
            AccessToken = accessToken,
            AccessTokenExpiresAt = expiresAt,
            RefreshToken = newRefreshToken.Token
        };

        return AuthResult.Succeeded(response);
    }

    public async Task<OperationResult> RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == refreshToken, cancellationToken);

        if (token is null || !token.IsActive)
        {
            return OperationResult.Failed("InvalidRefreshToken");
        }

        token.RevokedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Succeeded();
    }

    public async Task<OperationResult> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);

        if (user is null)
        {
            return OperationResult.Failed("UserNotFound");
        }

        if (user.IsEmailConfirmed)
        {
            return OperationResult.Succeeded();
        }

        if (!string.Equals(user.EmailConfirmationToken, request.Token, StringComparison.Ordinal))
        {
            return OperationResult.Failed("InvalidConfirmationToken");
        }

        user.IsEmailConfirmed = true;
        user.EmailConfirmationToken = null;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Succeeded();
    }

    public async Task<OperationResult> RequestPasswordResetAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);

        if (user is null)
        {
            return OperationResult.Succeeded();
        }

        user.PasswordResetToken = GenerateSecureToken(48);
        user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddHours(2);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Password reset token generated for {Email}", user.Email);
        return OperationResult.Succeeded();
    }

    public async Task<OperationResult> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);

        if (user is null)
        {
            return OperationResult.Failed("UserNotFound");
        }

        if (!string.Equals(user.PasswordResetToken, request.Token, StringComparison.Ordinal))
        {
            return OperationResult.Failed("InvalidResetToken");
        }

        if (user.PasswordResetTokenExpiresAt is null || user.PasswordResetTokenExpiresAt < DateTime.UtcNow)
        {
            return OperationResult.Failed("ResetTokenExpired");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiresAt = null;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return OperationResult.Succeeded();
    }

    private async Task<AuthResult> CreateAuthResultAsync(User user, CancellationToken cancellationToken)
    {
        var (accessToken, expiresAt) = _jwtTokenGenerator.GenerateToken(user, Array.Empty<Claim>());
        var refreshToken = CreateRefreshToken(user.Id);

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
            AccessToken = accessToken,
            AccessTokenExpiresAt = expiresAt,
            RefreshToken = refreshToken.Token
        };

        return AuthResult.Succeeded(response);
    }

    private RefreshToken CreateRefreshToken(Guid userId)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = GenerateSecureToken(64),
            ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenDays)
        };
    }

    private static string GenerateSecureToken(int size)
    {
        var bytes = RandomNumberGenerator.GetBytes(size);
        return Convert.ToBase64String(bytes);
    }
}
