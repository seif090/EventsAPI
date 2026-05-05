namespace EventsAPI.Application.Authentication.Models;

public class OperationResult
{
    public bool Success { get; set; }
    public IReadOnlyCollection<string> Errors { get; set; } = Array.Empty<string>();

    public static OperationResult Failed(params string[] errors) => new() { Success = false, Errors = errors };
    public static OperationResult Succeeded() => new() { Success = true };
}
