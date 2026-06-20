using System;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Describes filtering for AdminQuery.
/// Supports basic equality, contains, range, etc.
/// </summary>
public sealed class AdminFilterDescriptor
{
    public required string PropertyName { get; init; }
    public required AdminFilterOperator Operator { get; init; }
    public object? Value { get; init; }
    public object? Value2 { get; init; } // for range filters
}
