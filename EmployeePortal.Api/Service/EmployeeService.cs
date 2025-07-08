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
    }
}
