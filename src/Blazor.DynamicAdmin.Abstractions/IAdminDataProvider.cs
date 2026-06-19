using System.Threading;
using System.Threading.Tasks;

namespace Blazor.DynamicAdmin.Abstractions;

public interface IAdminDataProvider
{
    Task<AdminQueryResult> QueryAsync(AdminQueryContext context, CancellationToken cancellationToken = default);
    Task<object?> GetAsync(AdminGetContext context, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(AdminCreateContext context, CancellationToken cancellationToken = default);
    Task<object> UpdateAsync(AdminUpdateContext context, CancellationToken cancellationToken = default);
    Task DeleteAsync(AdminDeleteContext context, CancellationToken cancellationToken = default);
}

//this is a test
