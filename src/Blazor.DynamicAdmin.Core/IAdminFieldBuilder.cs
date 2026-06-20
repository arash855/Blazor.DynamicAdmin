using System;
using System.Linq.Expressions;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Generic fluent builder for configuring a single field (developer-friendly).
/// </summary>
public interface IAdminFieldBuilder<TEntity, TValue>
{
  IAdminFieldBuilder<TEntity, TValue> Label(string label);

  IAdminFieldBuilder<TEntity, TValue> Kind(AdminFieldKind kind);

  IAdminFieldBuilder<TEntity, TValue> VisibleInTable(bool visible = true);
  IAdminFieldBuilder<TEntity, TValue> VisibleInCreateForm(bool visible = true);
  IAdminFieldBuilder<TEntity, TValue> VisibleInEditForm(bool visible = true);
  IAdminFieldBuilder<TEntity, TValue> VisibleInDetails(bool visible = true);

  IAdminFieldBuilder<TEntity, TValue> ReadOnly(bool readOnly = true);

  IAdminFieldBuilder<TEntity, TValue> Order(int order);

  IAdminFieldBuilder<TEntity, TValue> Required(bool required = true);
  IAdminFieldBuilder<TEntity, TValue> Searchable(bool searchable = true);
  IAdminFieldBuilder<TEntity, TValue> Sortable(bool sortable = true);

  // For relations
  IAdminFieldBuilder<TEntity, TValue> Relation<TRelated>(Expression<Func<TValue, object>> displayProperty);

  // Custom configuration
  IAdminFieldBuilder<TEntity, TValue> Configure(Action<AdminFieldDescriptor> configure);

  // For fluent chaining
  IAdminResourceBuilder<TEntity> EndField();
}