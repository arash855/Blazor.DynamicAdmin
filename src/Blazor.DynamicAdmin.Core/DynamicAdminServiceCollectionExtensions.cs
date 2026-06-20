using System;
using Blazor.DynamicAdmin.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Extension methods for IServiceCollection to register DynamicAdmin services.
/// </summary>
public static class DynamicAdminServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicAdmin(this IServiceCollection services, Action<DynamicAdminOptions>? configure = null)
    {
        services.AddSingleton<AdminRegistry>();
        services.AddSingleton<IAdminMetadataProvider>(sp => sp.GetRequiredService<AdminRegistry>());
        services.AddSingleton<IAdminAccessorFactory, ExpressionAdminAccessorFactory>();

        // TODO: Register metadata builders, providers, etc.

        var options = new DynamicAdminOptions();
        configure?.Invoke(options);

        return services;
    }
}
