using EmployeePortal.Api.Core.Interfaces;
using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.Repository.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllPaginatedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken token);
    }
}
