namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Context for authorization checks.
/// </summary>
public sealed class AdminAuthorizationContext
{
    public required AdminResourceDescriptor Resource { get; init; }
    public required AdminOperation Operation { get; init; }
    public object? Entity { get; init; }
}
