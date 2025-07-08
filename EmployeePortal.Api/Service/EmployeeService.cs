using EmployeePortal.Api.Common;
using EmployeePortal.Api.Common.DTO;
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

        public async Task<PaginatedResult<Employee>> GetAllPaginatedAsync(
            EmployeeQueryParameters queryParameters,
            CancellationToken token)
        {
            return await _employeeRepository.GetAllPaginatedAsync(queryParameters, token);
        }
    }
}
