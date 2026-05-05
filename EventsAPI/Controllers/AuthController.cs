using EventsAPI.Application.Authentication.Contracts;
using EventsAPI.Application.Authentication.Models;
using EventsAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<AuthResponse>.Ok(result.Data!))
            : BadRequest(ApiResponse<AuthResponse>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<AuthResponse>.Ok(result.Data!))
            : Unauthorized(ApiResponse<AuthResponse>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<AuthResponse>.Ok(result.Data!))
            : Unauthorized(ApiResponse<AuthResponse>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<string>.Ok("Revoked"))
            : BadRequest(ApiResponse<string>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.ConfirmEmailAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<string>.Ok("Confirmed"))
            : BadRequest(ApiResponse<string>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RequestPasswordResetAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<string>.Ok("If the email exists, a reset token has been generated."))
            : BadRequest(ApiResponse<string>.Fail(result.Errors.ToArray()));
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.ResetPasswordAsync(request, cancellationToken);
        return result.Success
            ? Ok(ApiResponse<string>.Ok("PasswordReset"))
            : BadRequest(ApiResponse<string>.Fail(result.Errors.ToArray()));
    }
}
