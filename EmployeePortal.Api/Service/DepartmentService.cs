using EmployeePortal.Api.Core.BaseEntities;
using EmployeePortal.Api.Core.Interfaces;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using EmployeePortal.Api.Service.Interfaces;

namespace EmployeePortal.Api.Service;

public class DepartmentService: BaseService<Department>, IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
}