using Blazor.DynamicAdmin.Abstractions;
using Blazor.DynamicAdmin.Blazor.Components.Fields;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.DynamicAdmin.Blazor;

/// <summary>
/// Extension methods for registering Blazor UI services.
/// </summary>
public static class DynamicAdminBlazorServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicAdminBlazor(this IServiceCollection services)
    {
        services.AddSingleton<IAdminFieldComponentResolver, DefaultAdminFieldComponentResolver>();
        return services;
    }
}
