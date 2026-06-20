using System;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Flags defining CRUD capabilities for a resource.
/// </summary>
[Flags]
public enum AdminCrudCapabilities
{
    None = 0,
    List = 1,
    Details = 2,
    Create = 4,
    Edit = 8,
    Delete = 16,
    All = List | Details | Create | Edit | Delete
}
