using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Result of a query operation.
/// </summary>
public sealed class AdminQueryResult
{
    public required IReadOnlyList<object> Items { get; init; }
    public required long TotalCount { get; init; }
}
