namespace EventsAPI.Application.Authentication.Models;

public class AuthResult
{
    public bool Success { get; set; }
    public AuthResponse? Data { get; set; }
    public IReadOnlyCollection<string> Errors { get; set; } = Array.Empty<string>();

    public static AuthResult Failed(params string[] errors) => new() { Success = false, Errors = errors };
    public static AuthResult Succeeded(AuthResponse response) => new() { Success = true, Data = response };
}
