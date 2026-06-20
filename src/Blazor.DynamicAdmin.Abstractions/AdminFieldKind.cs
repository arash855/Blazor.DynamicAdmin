namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Defines the type/kind of an admin field for rendering decisions.
/// </summary>
public enum AdminFieldKind
{
    Text,
    Number,
    Boolean,
    Date,
    DateTime,
    Enum,
    Select,
    MultiSelect,
    Image,
    File,
    Currency,
    Color,
    Relation,
    Custom
}
