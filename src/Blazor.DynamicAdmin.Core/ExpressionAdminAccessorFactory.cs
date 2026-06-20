using System;
using System.Linq.Expressions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// High-performance compiled accessor factory using Expressions.
/// </summary>
public sealed class ExpressionAdminAccessorFactory : IAdminAccessorFactory
{
    public Func<object, object?> CreateGetter(Type entityType, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(object), "entity");
        var converted = Expression.Convert(parameter, entityType);
        var property = Expression.PropertyOrField(converted, propertyName);
        var boxed = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<object, object?>>(boxed, parameter).Compile();
    }

    public Action<object, object?> CreateSetter(Type entityType, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(object), "entity");
        var value = Expression.Parameter(typeof(object), "value");
        var convertedEntity = Expression.Convert(parameter, entityType);
        var convertedValue = Expression.Convert(value, GetPropertyType(entityType, propertyName));
        var property = Expression.PropertyOrField(convertedEntity, propertyName);
        var assign = Expression.Assign(property, convertedValue);

        return Expression.Lambda<Action<object, object?>>(assign, parameter, value).Compile();
    }

    private static Type GetPropertyType(Type entityType, string propertyName)
    {
        var prop = entityType.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found");
        return prop.PropertyType;
    }
}
