using EmployeePortal.Api.Common;
using EmployeePortal.Api.Core.Interfaces;
using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.Service.Interfaces
{
    public interface IEmployeeService: IService<Employee>
    {
        Task<PaginatedResult<Employee>> GetAllPaginatedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken token);
    }
}
