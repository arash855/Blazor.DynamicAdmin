using System.Threading;
using System.Threading.Tasks;

namespace Blazor.DynamicAdmin.Abstractions;

/// <summary>
/// Abstraction for data operations. Non-generic for runtime.
/// Providers (EF, API, etc.) implement this.
/// </summary>
public interface IAdminDataProvider
{
    Task<AdminQueryResult> QueryAsync(AdminQueryContext context, CancellationToken cancellationToken = default);
    Task<object?> GetAsync(AdminGetContext context, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(AdminCreateContext context, CancellationToken cancellationToken = default);
    Task<object> UpdateAsync(AdminUpdateContext context, CancellationToken cancellationToken = default);
    Task DeleteAsync(AdminDeleteContext context, CancellationToken cancellationToken = default);
}
