using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Central registry for all admin resources.
/// Thread-safe and cached.
/// </summary>
public sealed class AdminRegistry : IAdminMetadataProvider
{
    private readonly ConcurrentDictionary<Type, AdminResourceDescriptor> _resourcesByType = new();
    private readonly ConcurrentDictionary<string, AdminResourceDescriptor> _resourcesByName = new();

    public void Register(AdminResourceDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        _resourcesByType[descriptor.EntityType] = descriptor;
        _resourcesByName[descriptor.Name] = descriptor;
    }

    public AdminResourceDescriptor GetResource(Type entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);
        return _resourcesByType.GetValueOrDefault(entityType) 
            ?? throw new KeyNotFoundException($"No admin resource registered for type {entityType.FullName}");
    }

    public AdminResourceDescriptor? FindResource(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        return _resourcesByName.GetValueOrDefault(name);
    }

    public IReadOnlyList<AdminResourceDescriptor> GetResources()
    {
        return _resourcesByType.Values.ToList();
    }
}
