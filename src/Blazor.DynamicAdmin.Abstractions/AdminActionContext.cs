namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context passed to action handlers.
/// </summary>
public sealed class AdminActionContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public object? Entity { get; init; } // For row actions
    public IReadOnlyList<object>? Entities { get; init; } // For bulk
    public IServiceProvider Services { get; init; } = default!;
}
