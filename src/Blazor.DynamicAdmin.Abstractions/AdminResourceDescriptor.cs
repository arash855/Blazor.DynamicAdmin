using System;
using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Abstractions;

public sealed class AdminResourceDescriptor
{
    public required string Name { get; init; }
    public required Type EntityType { get; init; }
    public Type? KeyType { get; init; }

    public string? Route { get; init; }
    public string? Group { get; init; }
    public string? Icon { get; init; }

    public IReadOnlyList<AdminFieldDescriptor> Fields { get; init; } = [];
    public IReadOnlyList<AdminActionDescriptor> Actions { get; init; } = [];

    public AdminCrudCapabilities Capabilities { get; init; } = AdminCrudCapabilities.All;
}