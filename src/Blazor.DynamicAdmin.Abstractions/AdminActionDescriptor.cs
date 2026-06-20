using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Metadata for custom actions (row, bulk, global).
/// </summary>
public sealed class AdminActionDescriptor
{
    public required string Name { get; init; }
    public string? Label { get; init; }
    public string? Icon { get; init; }

    public AdminActionPlacement Placement { get; init; } = AdminActionPlacement.Row;
    public AdminActionStyle Style { get; init; } = AdminActionStyle.Primary;

    public Func<AdminActionContext, CancellationToken, Task<AdminActionResult>>? Handler { get; init; }
}
