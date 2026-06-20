namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for retrieving a single entity.
/// </summary>
public sealed class AdminGetContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required object Key { get; init; }
    public IServiceProvider Services { get; init; } = default!;
}
