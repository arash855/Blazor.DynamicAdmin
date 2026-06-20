namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Result of executing an action.
/// </summary>
public sealed class AdminActionResult
{
    public bool Success { get; init; } = true;
    public string? Message { get; init; }
    public object? Data { get; init; }
}
