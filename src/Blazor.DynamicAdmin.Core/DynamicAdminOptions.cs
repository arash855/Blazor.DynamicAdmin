using System;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Options for configuring DynamicAdmin.
/// </summary>
public sealed class DynamicAdminOptions
{
    internal readonly List<Action<AdminRegistry, AdminMetadataBuilder, IAdminDataProvider>> Registrations = [];

    public DynamicAdminOptions Resource<TEntity>(string? name = null, Action<IAdminResourceBuilder<TEntity>>? configure = null)
    {
        Registrations.Add((registry, metadataBuilder, defaultProvider) =>
        {
            var descriptor = metadataBuilder.Build<TEntity>(name, configure);
            registry.Register(AttachProvider(descriptor, defaultProvider));
        });

        return this;
    }

    private static AdminResourceDescriptor AttachProvider(AdminResourceDescriptor descriptor, IAdminDataProvider defaultProvider)
    {
        if (descriptor.DataProvider != null)
            return descriptor;

        return new AdminResourceDescriptor
        {
            Name = descriptor.Name,
            EntityType = descriptor.EntityType,
            KeyType = descriptor.KeyType,
            DisplayName = descriptor.DisplayName,
            PluralDisplayName = descriptor.PluralDisplayName,
            Route = descriptor.Route,
            Group = descriptor.Group,
            Icon = descriptor.Icon,
            Capabilities = descriptor.Capabilities,
            Fields = descriptor.Fields,
            Actions = descriptor.Actions,
            DataProvider = defaultProvider
        };
    }
}
