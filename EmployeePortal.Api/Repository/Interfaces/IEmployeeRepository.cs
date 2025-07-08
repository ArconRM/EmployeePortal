using EmployeePortal.Api.Common;
using EmployeePortal.Api.Common.DTO;
using EmployeePortal.Api.Core.Interfaces;
using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.Repository.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<PaginatedResult<Employee>> GetAllPaginatedAsync(
            EmployeeQueryParameters queryParameters,
            CancellationToken token);
    }
}
