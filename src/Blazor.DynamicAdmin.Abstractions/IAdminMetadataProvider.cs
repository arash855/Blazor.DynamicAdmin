using System;
using System.Collections.Generic;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Provides metadata for admin resources.
/// </summary>
public interface IAdminMetadataProvider
{
    AdminResourceDescriptor GetResource(Type entityType);
    AdminResourceDescriptor? FindResource(string name);
    IReadOnlyList<AdminResourceDescriptor> GetResources();
}
