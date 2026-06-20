namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for creating a new entity.
/// </summary>
public sealed class AdminCreateContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required object Model { get; init; }
    public IServiceProvider Services { get; init; } = default!;
}
