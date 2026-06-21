using System.Threading;
using System.Threading.Tasks;
using Blazor.DynamicAdmin.Abstractions;

namespace Blazor.DynamicAdmin.Core;

/// <summary>
/// Default authorization service that allows all operations.
/// </summary>
public sealed class AllowAllAdminAuthorizationService : IAdminAuthorizationService
{
    public Task<bool> IsGrantedAsync(AdminAuthorizationContext context, CancellationToken cancellationToken = default)
        => Task.FromResult(true);
}
