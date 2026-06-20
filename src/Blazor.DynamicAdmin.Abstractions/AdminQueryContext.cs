namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for querying a resource (list view).
/// </summary>
public sealed class AdminQueryContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required AdminQuery Query { get; init; }
    public IServiceProvider Services { get; init; } = default!;
}
