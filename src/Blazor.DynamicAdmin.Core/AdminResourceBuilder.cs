using System;
using System.Linq.Expressions;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Default implementation of fluent resource builder.
/// </summary>
public sealed class AdminResourceBuilder<TEntity> : IAdminResourceBuilder<TEntity>
{
    private readonly ResourceBuildContext _context;
    private readonly IAdminAccessorFactory _accessorFactory;

    internal AdminResourceBuilder(ResourceBuildContext context, IAdminAccessorFactory accessorFactory)
    {
        _context = context;
        _accessorFactory = accessorFactory;
    }

    public IAdminResourceBuilder<TEntity> DisplayName(string displayName)
    {
        _context.DisplayName = displayName;
        return this;
    }

    public IAdminResourceBuilder<TEntity> PluralDisplayName(string pluralDisplayName)
    {
        _context.PluralDisplayName = pluralDisplayName;
        return this;
    }

    public IAdminResourceBuilder<TEntity> Route(string route)
    {
        _context.Route = route;
        return this;
    }

    public IAdminResourceBuilder<TEntity> Group(string group)
    {
        _context.Group = group;
        return this;
    }

    public IAdminResourceBuilder<TEntity> Icon(string icon)
    {
        _context.Icon = icon;
        return this;
    }

    public IAdminResourceBuilder<TEntity> Capabilities(AdminCrudCapabilities capabilities)
    {
        _context.Capabilities = capabilities;
        return this;
    }

    public IAdminFieldBuilder<TEntity, TValue> Field<TValue>(Expression<Func<TEntity, TValue>> propertyExpression)
    {
        var propertyName = GetPropertyName(propertyExpression);
        var field = _context.Fields.FirstOrDefault(f => f.PropertyName == propertyName)
            ?? throw new InvalidOperationException($"Field '{propertyName}' was not discovered on {typeof(TEntity).Name}.");

        return new AdminFieldBuilder<TEntity, TValue>(this, field);
    }

    public IAdminResourceBuilder<TEntity> Field(string propertyName, Action<IAdminFieldBuilder<object, object>> configure)
    {
        throw new NotSupportedException("Use the strongly typed Field<TValue> overload.");
    }

    public IAdminResourceBuilder<TEntity> DisableCreate()
    {
        _context.Capabilities &= ~AdminCrudCapabilities.Create;
        return this;
    }

    public IAdminResourceBuilder<TEntity> DisableEdit()
    {
        _context.Capabilities &= ~AdminCrudCapabilities.Edit;
        return this;
    }

    public IAdminResourceBuilder<TEntity> DisableDelete()
    {
        _context.Capabilities &= ~AdminCrudCapabilities.Delete;
        return this;
    }

    internal void UpdateField(string propertyName, Func<AdminFieldDescriptor, AdminFieldDescriptor> update)
    {
        var index = _context.Fields.FindIndex(f => f.PropertyName == propertyName);
        if (index >= 0)
            _context.Fields[index] = update(_context.Fields[index]);
    }

    internal AdminResourceDescriptor Build() => _context.ToDescriptor();

    private static string GetPropertyName<TValue>(Expression<Func<TEntity, TValue>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression { Operand: MemberExpression unaryMember })
            return unaryMember.Member.Name;

        throw new ArgumentException("Expression must be a property access.", nameof(expression));
    }
}
