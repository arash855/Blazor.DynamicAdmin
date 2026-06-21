using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Blazor;

/// <summary>
/// Navigation request emitted by admin pages.
/// </summary>
public sealed record AdminNavigationRequest(string ResourceName, AdminPageMode Mode, object? Key = null);
