using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// In-memory entity store used by <see cref="InMemoryAdminDataProvider"/>.
/// </summary>
public sealed class InMemoryAdminDataStore
{
    private readonly ConcurrentDictionary<Type, List<object>> _stores = new();

    public List<object> GetOrCreateStore(Type entityType) =>
        _stores.GetOrAdd(entityType, _ => []);

    public void Seed<TEntity>(IEnumerable<TEntity> items) where TEntity : class
    {
        var store = GetOrCreateStore(typeof(TEntity));
        store.AddRange(items);
    }

    public void Clear(Type entityType)
    {
        if (_stores.TryGetValue(entityType, out var store))
            store.Clear();
    }
}
