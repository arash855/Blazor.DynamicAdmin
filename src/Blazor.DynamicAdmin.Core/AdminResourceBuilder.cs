using System;
using System.Linq.Expressions;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Default implementation of fluent resource builder.
/// </summary>
public sealed class AdminResourceBuilder<TEntity> : IAdminResourceBuilder<TEntity>
{
    private readonly AdminResourceDescriptor _descriptor;
    private readonly IAdminAccessorFactory _accessorFactory;

    internal AdminResourceBuilder(Type entityType, AdminResourceDescriptor descriptor, IAdminAccessorFactory accessorFactory)
    {
        _descriptor = descriptor;
        _accessorFactory = accessorFactory;
    }

    public IAdminResourceBuilder<TEntity> DisplayName(string displayName)
    {
        // TODO: update descriptor (immutable? use builder pattern properly)
        return this;
    }

    // Implement other methods similarly...
    public IAdminFieldBuilder<TEntity, TValue> Field<TValue>(Expression<Func<TEntity, TValue>> propertyExpression)
    {
        // TODO: full implementation
        throw new NotImplementedException("Field builder to be completed in next iteration");
    }

    public IAdminResourceBuilder<TEntity> Field(string propertyName, Action<IAdminFieldBuilder<object, object>> configure)
    {
        throw new NotImplementedException();
    }

    // Other methods...
    public IAdminResourceBuilder<TEntity> DisableCreate() => this;
    public IAdminResourceBuilder<TEntity> DisableEdit() => this;
    public IAdminResourceBuilder<TEntity> DisableDelete() => this;
    public IAdminResourceBuilder<TEntity> Route(string route) => this;
    public IAdminResourceBuilder<TEntity> Group(string group) => this;
    public IAdminResourceBuilder<TEntity> Icon(string icon) => this;
    public IAdminResourceBuilder<TEntity> Capabilities(AdminCrudCapabilities capabilities) => this;
}
