namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Operations that can be authorized.
/// </summary>
public enum AdminOperation
{
    List,
    Details,
    Create,
    Edit,
    Delete,
    ExecuteAction
}
