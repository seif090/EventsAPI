using EventsAPI.Application.Authentication.Models;

namespace EventsAPI.Application.Authentication.Contracts;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<OperationResult> RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<OperationResult> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken);
    Task<OperationResult> RequestPasswordResetAsync(ForgotPasswordRequest request, CancellationToken cancellationToken);
    Task<OperationResult> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken);
}
