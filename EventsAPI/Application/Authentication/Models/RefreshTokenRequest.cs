namespace EventsAPI.Application.Authentication.Models;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}
