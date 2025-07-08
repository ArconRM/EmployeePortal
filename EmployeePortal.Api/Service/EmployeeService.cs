using EmployeePortal.Api.Core.BaseEntities;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using EmployeePortal.Api.Service.Interfaces;

namespace EmployeePortal.Api.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository): base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            return await _employeeRepository.GetAllPaginatedAsync(pageNumber, pageSize, token);
        }
    }
}
