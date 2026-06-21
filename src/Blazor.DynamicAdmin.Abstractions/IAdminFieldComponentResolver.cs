using System;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Resolves Blazor component types for displaying and editing fields.
/// Implemented by UI packages (Blazor defaults, MudBlazor, etc.).
/// </summary>
public interface IAdminFieldComponentResolver
{
    Type ResolveDisplayComponent(AdminFieldDescriptor field);
    Type ResolveEditorComponent(AdminFieldDescriptor field);
}
