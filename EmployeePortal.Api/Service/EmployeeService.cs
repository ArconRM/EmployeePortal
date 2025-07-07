using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using EmployeePortal.Api.Service.Interfaces;

namespace EmployeePortal.Api.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken token)
        {
            return await _employeeRepository.CreateAsync(employee, token);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token)
        {
            return await _employeeRepository.GetAllAsync(token);
        }

        public async Task<Employee> GetEmployeeAsync(Guid uuid, CancellationToken token)
        {
            return await _employeeRepository.GetAsync(uuid, token);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken token)
        {
            return await _employeeRepository.UpdateAsync(employee, token);
        }

        public async Task DeleteEmployeeAsync(Guid uuid, CancellationToken token)
        {
            await _employeeRepository.DeleteAsync(uuid, token);
        }

    }
}
