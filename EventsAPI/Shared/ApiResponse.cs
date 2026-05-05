namespace EventsAPI.Shared;

public record ApiResponse<T>(bool Success, T? Data, IReadOnlyCollection<string> Errors)
{
    public static ApiResponse<T> Ok(T data) => new(true, data, Array.Empty<string>());
    public static ApiResponse<T> Fail(params string[] errors) => new(false, default, errors);
}
