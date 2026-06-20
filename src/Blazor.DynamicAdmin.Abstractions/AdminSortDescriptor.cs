using System;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Describes sorting for AdminQuery.
/// </summary>
public sealed class AdminSortDescriptor
{
    public required string PropertyName { get; init; }
    public bool Descending { get; init; } = false;
}
