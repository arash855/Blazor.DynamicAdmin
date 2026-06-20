using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Options for configuring DynamicAdmin.
/// </summary>
public sealed class DynamicAdminOptions
{
    // Fluent resource registration will be added here
    internal readonly List<Action<AdminRegistry>> _resourceRegistrations = new();

    public DynamicAdminOptions Resource<TEntity>(string? name = null, Action<IAdminResourceBuilder<TEntity>>? configure = null)
    {
        // Registration logic
        return this;
    }
}
