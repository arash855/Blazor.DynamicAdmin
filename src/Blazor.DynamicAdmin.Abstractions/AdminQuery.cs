using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Query parameters for admin list operations.
/// Supports pagination, search, sort, filter.
/// </summary>
public sealed class AdminQuery
{
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 20;

    public string? Search { get; init; }

    public IReadOnlyList<AdminSortDescriptor> Sorts { get; init; } = Array.Empty<AdminSortDescriptor>();
    public IReadOnlyList<AdminFilterDescriptor> Filters { get; init; } = Array.Empty<AdminFilterDescriptor>();
}
