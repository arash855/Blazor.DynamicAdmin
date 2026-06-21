using System;
using System.Linq.Expressions;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Fluent builder for configuring a single field.
/// </summary>
public sealed class AdminFieldBuilder<TEntity, TValue> : IAdminFieldBuilder<TEntity, TValue>
{
    private readonly AdminResourceBuilder<TEntity> _resourceBuilder;
    private readonly string _propertyName;

    internal AdminFieldBuilder(AdminResourceBuilder<TEntity> resourceBuilder, AdminFieldDescriptor field)
    {
        _resourceBuilder = resourceBuilder;
        _propertyName = field.PropertyName;
    }

    public IAdminFieldBuilder<TEntity, TValue> Label(string label) => Apply(f => Copy(f, label: label));
    public IAdminFieldBuilder<TEntity, TValue> Kind(AdminFieldKind kind) => Apply(f => Copy(f, kind: kind));
    public IAdminFieldBuilder<TEntity, TValue> VisibleInTable(bool visible = true) => Apply(f => Copy(f, visibleInTable: visible));
    public IAdminFieldBuilder<TEntity, TValue> VisibleInCreateForm(bool visible = true) => Apply(f => Copy(f, visibleInCreateForm: visible));
    public IAdminFieldBuilder<TEntity, TValue> VisibleInEditForm(bool visible = true) => Apply(f => Copy(f, visibleInEditForm: visible));
    public IAdminFieldBuilder<TEntity, TValue> VisibleInDetails(bool visible = true) => Apply(f => Copy(f, visibleInTable: visible));
    public IAdminFieldBuilder<TEntity, TValue> ReadOnly(bool readOnly = true) => Apply(f => Copy(f, readOnly: readOnly));
    public IAdminFieldBuilder<TEntity, TValue> Order(int order) => Apply(f => Copy(f, order: order));
    public IAdminFieldBuilder<TEntity, TValue> Required(bool required = true) => this;
    public IAdminFieldBuilder<TEntity, TValue> Searchable(bool searchable = true) => this;
    public IAdminFieldBuilder<TEntity, TValue> Sortable(bool sortable = true) => this;

    public IAdminFieldBuilder<TEntity, TValue> Relation<TRelated>(Expression<Func<TValue, object>> displayProperty)
        => Apply(f => Copy(f, kind: AdminFieldKind.Relation));

    public IAdminFieldBuilder<TEntity, TValue> Configure(Action<AdminFieldDescriptor> configure) => this;

    public IAdminResourceBuilder<TEntity> EndField() => _resourceBuilder;

    private IAdminFieldBuilder<TEntity, TValue> Apply(Func<AdminFieldDescriptor, AdminFieldDescriptor> transform)
    {
        _resourceBuilder.UpdateField(_propertyName, transform);
        return this;
    }

    private static AdminFieldDescriptor Copy(
        AdminFieldDescriptor source,
        string? label = null,
        AdminFieldKind? kind = null,
        bool? visibleInTable = null,
        bool? visibleInCreateForm = null,
        bool? visibleInEditForm = null,
        bool? readOnly = null,
        int? order = null) => new()
    {
        Name = source.Name,
        PropertyName = source.PropertyName,
        ValueType = source.ValueType,
        Label = label ?? source.Label,
        Kind = kind ?? source.Kind,
        VisibleInTable = visibleInTable ?? source.VisibleInTable,
        VisibleInCreateForm = visibleInCreateForm ?? source.VisibleInCreateForm,
        VisibleInEditForm = visibleInEditForm ?? source.VisibleInEditForm,
        ReadOnly = readOnly ?? source.ReadOnly,
        Order = order ?? source.Order,
        GetValue = source.GetValue,
        SetValue = source.SetValue
    };
}
