using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken token);

        public Task<Employee> GetEmployeeAsync(Guid uuid, CancellationToken token);

        public Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token);

        public Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken token);

        public Task DeleteEmployeeAsync(Guid uuid, CancellationToken token);
    }
}
