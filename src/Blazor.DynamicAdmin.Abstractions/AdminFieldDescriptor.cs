using System;

namespace Blazor.DynamicAdmin.Abstractions;

public sealed class AdminFieldDescriptor
{
    public required string Name { get; init; }
    public required string PropertyName { get; init; }
    public required Type ValueType { get; init; }

    public string? Label { get; init; }
    public AdminFieldKind Kind { get; init; } = AdminFieldKind.Text;

    public bool VisibleInTable { get; init; } = true;
    public bool VisibleInCreateForm { get; init; } = true;
    public bool VisibleInEditForm { get; init; } = true;
    public bool ReadOnly { get; init; }

    public int Order { get; init; }

    public Func<object, object?>? GetValue { get; init; }
    public Action<object, object?>? SetValue { get; init; }
}

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