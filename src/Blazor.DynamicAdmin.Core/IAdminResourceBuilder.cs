using System;
using System.Linq.Expressions;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Generic fluent builder for configuring a resource (developer-friendly).
/// </summary>
public interface IAdminResourceBuilder<TEntity>
{
    IAdminResourceBuilder<TEntity> DisplayName(string displayName);
    IAdminResourceBuilder<TEntity> PluralDisplayName(string pluralDisplayName);
    IAdminResourceBuilder<TEntity> Route(string route);
    IAdminResourceBuilder<TEntity> Group(string group);
    IAdminResourceBuilder<TEntity> Icon(string icon);
    IAdminResourceBuilder<TEntity> Capabilities(AdminCrudCapabilities capabilities);

    IAdminFieldBuilder<TEntity, TValue> Field<TValue>(Expression<Func<TEntity, TValue>> propertyExpression);
    IAdminResourceBuilder<TEntity> Field(string propertyName, Action<IAdminFieldBuilder<object, object>> configure);

    IAdminResourceBuilder<TEntity> DisableCreate();
    IAdminResourceBuilder<TEntity> DisableEdit();
    IAdminResourceBuilder<TEntity> DisableDelete();
}
