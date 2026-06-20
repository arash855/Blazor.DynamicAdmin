namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Filter operators supported in AdminQuery.
/// </summary>
public enum AdminFilterOperator
{
    Equals,
    NotEquals,
    Contains,
    StartsWith,
    EndsWith,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    In,
    IsNull,
    IsNotNull
}
