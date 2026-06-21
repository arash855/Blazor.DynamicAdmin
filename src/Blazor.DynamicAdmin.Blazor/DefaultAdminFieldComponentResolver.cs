using Blazor.DynamicAdmin.Abstractions;
using Blazor.DynamicAdmin.Blazor.Components.Fields;

namespace Blazor.DynamicAdmin.Blazor;

/// <summary>
/// Default field component resolver using built-in HTML-based renderers.
/// </summary>
public sealed class DefaultAdminFieldComponentResolver : IAdminFieldComponentResolver
{
    public Type ResolveDisplayComponent(AdminFieldDescriptor field) => field.Kind switch
    {
        AdminFieldKind.Boolean => typeof(BooleanDisplayField),
        AdminFieldKind.Number or AdminFieldKind.Currency => typeof(NumberDisplayField),
        AdminFieldKind.Date or AdminFieldKind.DateTime => typeof(DateTimeDisplayField),
        AdminFieldKind.Enum => typeof(TextDisplayField),
        _ => typeof(TextDisplayField)
    };

    public Type ResolveEditorComponent(AdminFieldDescriptor field)
    {
        if (field.ReadOnly)
            return ResolveDisplayComponent(field);

        return field.Kind switch
        {
            AdminFieldKind.Boolean => typeof(BooleanEditorField),
            AdminFieldKind.Number or AdminFieldKind.Currency => typeof(NumberEditorField),
            AdminFieldKind.Date or AdminFieldKind.DateTime => typeof(DateTimeEditorField),
            AdminFieldKind.Enum => typeof(EnumEditorField),
            _ => typeof(TextEditorField)
        };
    }
}
