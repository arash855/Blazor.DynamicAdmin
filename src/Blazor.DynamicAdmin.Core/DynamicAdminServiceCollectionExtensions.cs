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
        var options = new DynamicAdminOptions();
        configure?.Invoke(options);
        services.AddSingleton(options);

        services.AddSingleton<AdminRegistry>(sp => CreateRegistry(sp, options));
        services.AddSingleton<IAdminMetadataProvider>(sp => sp.GetRequiredService<AdminRegistry>());
        services.AddSingleton<IAdminAccessorFactory, ExpressionAdminAccessorFactory>();
        services.AddSingleton<AdminMetadataBuilder>();
        services.AddSingleton<InMemoryAdminDataStore>();
        services.AddSingleton<IAdminDataProvider, InMemoryAdminDataProvider>();
        services.AddSingleton<IAdminAuthorizationService, AllowAllAdminAuthorizationService>();

        return services;
    }

    private static AdminRegistry CreateRegistry(IServiceProvider sp, DynamicAdminOptions options)
    {
        var registry = new AdminRegistry();
        var metadataBuilder = sp.GetRequiredService<AdminMetadataBuilder>();
        var defaultProvider = sp.GetRequiredService<IAdminDataProvider>();

        foreach (var registration in options.Registrations)
            registration(registry, metadataBuilder, defaultProvider);

        return registry;
    }
}
