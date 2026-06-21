using Blazor.DynamicAdmin.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Blazor.DynamicAdmin.Blazor.Components.Fields;

/// <summary>
/// Base type for field display components.
/// </summary>
public abstract class AdminFieldDisplayBase : ComponentBase
{
    [Parameter, EditorRequired] public AdminFieldDescriptor Field { get; set; } = default!;
    [Parameter, EditorRequired] public object Item { get; set; } = default!;

    protected object? Value => Field.GetValue?.Invoke(Item);
}

/// <summary>
/// Base type for field editor components.
/// </summary>
public abstract class AdminFieldEditorBase : ComponentBase
{
    [Parameter, EditorRequired] public AdminFieldDescriptor Field { get; set; } = default!;
    [Parameter, EditorRequired] public object Model { get; set; } = default!;

    protected object? Value
    {
        get => Field.GetValue?.Invoke(Model);
        set => Field.SetValue?.Invoke(Model, value);
    }
}
