namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for updating an entity.
/// </summary>
public sealed class AdminUpdateContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required object Model { get; init; }
    public IServiceProvider Services { get; init; } = default!;
}
