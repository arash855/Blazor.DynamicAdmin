using System;
using System.Collections.Generic;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Mutable state used while building a resource descriptor.
/// </summary>
internal sealed class ResourceBuildContext
{
    public required string Name { get; set; }
    public required Type EntityType { get; set; }
    public Type? KeyType { get; set; }
    public string? DisplayName { get; set; }
    public string? PluralDisplayName { get; set; }
    public string? Route { get; set; }
    public string? Group { get; set; }
    public string? Icon { get; set; }
    public AdminCrudCapabilities Capabilities { get; set; } = AdminCrudCapabilities.All;
    public List<AdminFieldDescriptor> Fields { get; set; } = [];
    public List<AdminActionDescriptor> Actions { get; set; } = [];
    public IAdminDataProvider? DataProvider { get; set; }

    public AdminResourceDescriptor ToDescriptor()
    {
        return new AdminResourceDescriptor
        {
            Name = Name,
            EntityType = EntityType,
            KeyType = KeyType,
            DisplayName = DisplayName ?? Name,
            PluralDisplayName = PluralDisplayName ?? DisplayName ?? Name,
            Route = Route ?? Name.ToLowerInvariant(),
            Group = Group,
            Icon = Icon,
            Capabilities = Capabilities,
            Fields = Fields,
            Actions = Actions,
            DataProvider = DataProvider
        };
    }
}
