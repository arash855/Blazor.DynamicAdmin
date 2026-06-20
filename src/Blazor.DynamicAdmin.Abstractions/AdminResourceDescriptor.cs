using System;
using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Represents metadata for an admin resource (entity).
/// Non-generic for runtime/UI usage.
/// </summary>
public sealed class AdminResourceDescriptor
{
    public required string Name { get; init; }
    public required Type EntityType { get; init; }
    public Type? KeyType { get; init; }

    public string? DisplayName { get; init; }
    public string? PluralDisplayName { get; init; }

    public string? Route { get; init; }
    public string? Group { get; init; }
    public string? Icon { get; init; }

    public AdminCrudCapabilities Capabilities { get; init; } = AdminCrudCapabilities.All;

    public IReadOnlyList<AdminFieldDescriptor> Fields { get; init; } = Array.Empty<AdminFieldDescriptor>();
    public IReadOnlyList<AdminActionDescriptor> Actions { get; init; } = Array.Empty<AdminActionDescriptor>();

    public IAdminDataProvider? DataProvider { get; init; }
}
