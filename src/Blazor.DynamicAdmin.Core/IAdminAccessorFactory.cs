using System;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Factory for compiled property accessors (performance critical).
/// </summary>
public interface IAdminAccessorFactory
{
    Func<object, object?> CreateGetter(Type entityType, string propertyName);
    Action<object, object?> CreateSetter(Type entityType, string propertyName);
}
