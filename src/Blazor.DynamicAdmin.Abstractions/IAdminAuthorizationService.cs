using System.Threading;
using System.Threading.Tasks;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Authorization abstraction for admin operations.
/// </summary>
public interface IAdminAuthorizationService
{
    Task<bool> IsGrantedAsync(AdminAuthorizationContext context, CancellationToken cancellationToken = default);
}
