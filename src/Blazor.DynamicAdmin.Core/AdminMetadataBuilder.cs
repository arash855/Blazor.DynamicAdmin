using System;
using System.Collections.Generic;
using System.Reflection;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Builds metadata from convention + attributes + fluent configuration.
/// Reflection happens only at startup.
/// </summary>
public sealed class AdminMetadataBuilder
{
    private readonly IAdminAccessorFactory _accessorFactory;

    public AdminMetadataBuilder(IAdminAccessorFactory accessorFactory)
    {
        _accessorFactory = accessorFactory ?? throw new ArgumentNullException(nameof(accessorFactory));
    }

    public AdminResourceDescriptor Build<TEntity>(string? name = null, Action<IAdminResourceBuilder<TEntity>>? configure = null)
    {
        var context = CreateContext(typeof(TEntity), name);

        if (configure != null)
        {
            var builder = new AdminResourceBuilder<TEntity>(context, _accessorFactory);
            configure(builder);
            return builder.Build();
        }

        return context.ToDescriptor();
    }

    internal ResourceBuildContext CreateContext(Type entityType, string? name = null)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        return new ResourceBuildContext
        {
            Name = name ?? entityType.Name,
            EntityType = entityType,
            KeyType = FindKeyType(entityType),
            DisplayName = entityType.Name,
            Fields = BuildFields(entityType).ToList()
        };
    }

    private static Type? FindKeyType(Type entityType)
    {
        var keyProp = entityType.GetProperty("Id")
            ?? entityType.GetProperties().FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.Ordinal));

        return keyProp?.PropertyType;
    }

    private IEnumerable<AdminFieldDescriptor> BuildFields(Type entityType)
    {
        foreach (var prop in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.GetCustomAttribute<AdminIgnoreAttribute>() != null)
                continue;

            yield return new AdminFieldDescriptor
            {
                Name = prop.Name,
                PropertyName = prop.Name,
                ValueType = prop.PropertyType,
                Label = prop.Name,
                Kind = DetermineFieldKind(prop.PropertyType),
                GetValue = _accessorFactory.CreateGetter(entityType, prop.Name),
                SetValue = _accessorFactory.CreateSetter(entityType, prop.Name)
            };
        }
    }

    private static AdminFieldKind DetermineFieldKind(Type type)
    {
        var underlying = Nullable.GetUnderlyingType(type) ?? type;

        if (underlying == typeof(bool)) return AdminFieldKind.Boolean;
        if (underlying == typeof(DateTime)) return AdminFieldKind.DateTime;
        if (underlying == typeof(DateOnly)) return AdminFieldKind.Date;
        if (underlying.IsEnum) return AdminFieldKind.Enum;
        if (underlying == typeof(decimal) || underlying == typeof(double) || underlying == typeof(float))
            return AdminFieldKind.Number;
        if (underlying == typeof(int) || underlying == typeof(long) || underlying == typeof(short))
            return AdminFieldKind.Number;

        return AdminFieldKind.Text;
    }
}
