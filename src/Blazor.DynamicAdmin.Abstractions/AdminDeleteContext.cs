namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for deleting an entity.
/// </summary>
public sealed class AdminDeleteContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required object Key { get; init; }
    public IServiceProvider Services { get; init; } = default!;
}
