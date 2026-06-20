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

    public AdminResourceDescriptor Build(Type entityType, Action<IAdminResourceBuilder<object>>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        // TODO: Convention + Attribute discovery + Fluent override
        var descriptor = new AdminResourceDescriptor
        {
            Name = entityType.Name,
            EntityType = entityType,
            KeyType = FindKeyType(entityType),
            Fields = BuildFields(entityType).ToList(),
            // Actions, etc. later
        };

        // Apply fluent configuration if provided
        if (configure != null)
        {
            var builder = new AdminResourceBuilder<object>(entityType, descriptor, _accessorFactory);
            configure(builder);
            // Merge back changes
        }

        return descriptor;
    }

    private Type? FindKeyType(Type entityType)
    {
        // Simple convention: property named Id or EntityNameId
        var keyProp = entityType.GetProperty("Id") ?? 
                     entityType.GetProperties().FirstOrDefault(p => p.Name.EndsWith("Id"));
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
                SetValue = _accessorFactory.CreateSetter(entityType, prop.Name),
                VisibleInTable = true,
                VisibleInCreateForm = true,
                VisibleInEditForm = true
            };
        }
    }

    private static AdminFieldKind DetermineFieldKind(Type type)
    {
        if (type == typeof(bool)) return AdminFieldKind.Boolean;
        if (type == typeof(DateTime)) return AdminFieldKind.DateTime;
        if (type == typeof(decimal) || type == typeof(double)) return AdminFieldKind.Currency;
        // ... more later
        return AdminFieldKind.Text;
    }
}
