using System;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Marks a property to be ignored in the DynamicAdmin metadata (not shown in tables, forms, etc.).
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class AdminIgnoreAttribute : Attribute
{
  // Marker attribute - no additional properties needed for now
}