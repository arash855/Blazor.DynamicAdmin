using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// In-memory data provider for development and testing.
/// </summary>
public sealed class InMemoryAdminDataProvider : IAdminDataProvider
{
    private readonly InMemoryAdminDataStore _store;

    public InMemoryAdminDataProvider(InMemoryAdminDataStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public Task<AdminQueryResult> QueryAsync(AdminQueryContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        var items = _store.GetOrCreateStore(context.Resource.EntityType).ToList();

        if (!string.IsNullOrWhiteSpace(context.Query.Search))
        {
            var search = context.Query.Search;
            items = items.Where(item => MatchesSearch(context.Resource, item, search)).ToList();
        }

        foreach (var sort in context.Query.Sorts)
            items = ApplySort(items, context.Resource, sort).ToList();

        var totalCount = items.Count;
        var pageIndex = Math.Max(0, context.Query.PageIndex);
        var pageSize = Math.Max(1, context.Query.PageSize);
        var page = items.Skip(pageIndex * pageSize).Take(pageSize).ToList();

        return Task.FromResult(new AdminQueryResult
        {
            Items = page,
            TotalCount = totalCount
        });
    }

    public Task<object?> GetAsync(AdminGetContext context, CancellationToken cancellationToken = default)
    {
        var items = _store.GetOrCreateStore(context.Resource.EntityType);
        var match = items.FirstOrDefault(item => KeysEqual(context.Resource, item, context.Key));
        return Task.FromResult(match);
    }

    public Task<object> CreateAsync(AdminCreateContext context, CancellationToken cancellationToken = default)
    {
        var items = _store.GetOrCreateStore(context.Resource.EntityType);
        EnsureKey(context.Resource, context.Model);
        items.Add(context.Model);
        return Task.FromResult(context.Model);
    }

    public Task<object> UpdateAsync(AdminUpdateContext context, CancellationToken cancellationToken = default)
    {
        var items = _store.GetOrCreateStore(context.Resource.EntityType);
        var key = GetKeyValue(context.Resource, context.Model)
            ?? throw new InvalidOperationException("Updated model does not contain a key value.");

        var index = items.FindIndex(item => KeysEqual(context.Resource, item, key));
        if (index < 0)
            throw new KeyNotFoundException($"Entity with key '{key}' was not found.");

        items[index] = context.Model;
        return Task.FromResult(context.Model);
    }

    public Task DeleteAsync(AdminDeleteContext context, CancellationToken cancellationToken = default)
    {
        var items = _store.GetOrCreateStore(context.Resource.EntityType);
        items.RemoveAll(item => KeysEqual(context.Resource, item, context.Key));
        return Task.CompletedTask;
    }

    private static bool MatchesSearch(AdminResourceDescriptor resource, object item, string search)
    {
        foreach (var field in resource.Fields)
        {
            if (field.GetValue?.Invoke(item) is string text &&
                text.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static List<object> ApplySort(List<object> items, AdminResourceDescriptor resource, AdminSortDescriptor sort)
    {
        var field = resource.Fields.FirstOrDefault(f => f.PropertyName == sort.PropertyName);
        if (field?.GetValue == null)
            return items;

        return sort.Descending
            ? items.OrderByDescending(field.GetValue, Comparer<object?>.Default).ToList()
            : items.OrderBy(field.GetValue, Comparer<object?>.Default).ToList();
    }

    private void EnsureKey(AdminResourceDescriptor resource, object model)
    {
        var key = GetKeyValue(resource, model);
        if (key != null && !IsDefaultKey(key))
            return;

        var keyProperty = GetKeyProperty(resource);
        if (keyProperty == null)
            return;

        object? generated = keyProperty.PropertyType switch
        {
            Type t when t == typeof(Guid) => Guid.NewGuid(),
            Type t when t == typeof(int) => GenerateIntKey(resource),
            Type t when t == typeof(long) => (long)GenerateIntKey(resource),
            _ => null
        };

        if (generated != null)
            keyProperty.SetValue(model, generated);
    }

    private int GenerateIntKey(AdminResourceDescriptor resource)
    {
        return _store.GetOrCreateStore(resource.EntityType)
            .Select(item => GetKeyValue(resource, item))
            .OfType<int>()
            .DefaultIfEmpty(0)
            .Max() + 1;
    }

    private static bool IsDefaultKey(object key) => key switch
    {
        Guid guid => guid == Guid.Empty,
        int i => i == 0,
        long l => l == 0L,
        string s => string.IsNullOrEmpty(s),
        _ => false
    };

    private static bool KeysEqual(AdminResourceDescriptor resource, object item, object key)
    {
        var itemKey = GetKeyValue(resource, item);
        return itemKey != null && itemKey.Equals(key);
    }

    private static object? GetKeyValue(AdminResourceDescriptor resource, object item)
    {
        var keyProperty = GetKeyProperty(resource);
        return keyProperty?.GetValue(item);
    }

    private static PropertyInfo? GetKeyProperty(AdminResourceDescriptor resource)
    {
        var entityType = resource.EntityType;
        return entityType.GetProperty("Id")
            ?? entityType.GetProperties().FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.Ordinal));
    }
}
