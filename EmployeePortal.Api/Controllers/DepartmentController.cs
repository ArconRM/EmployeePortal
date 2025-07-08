using AutoMapper;
using EmployeePortal.Api.Common.DTO;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Api.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost(nameof(CreateDepartment))]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDTO departmentDto, CancellationToken token)
        {
            try
            {
                Department department = _mapper.Map<DepartmentDTO, Department>(departmentDto);
                Department newDepartment = await _departmentService.CreateAsync(department, token);
                DepartmentDTO newDepartmentDto = _mapper.Map<Department, DepartmentDTO>(newDepartment);
                return Ok(newDepartmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet(nameof(GetAllDepartments))]
        public async Task<IActionResult> GetAllDepartments(CancellationToken token)
        {
            try
            {
                IEnumerable<Department> departments = await _departmentService.GetAllAsync(token);
                IEnumerable<DepartmentDTO> departmentsDto = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(departments);
                return Ok(departmentsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet(nameof(GetDepartment))]
        public async Task<IActionResult> GetDepartment(Guid id, CancellationToken token)
        {
            try
            {
                var department = await _departmentService.GetAsync(id, token);
                var departmentDto = _mapper.Map<Department, DepartmentDTO>(department);
                return Ok(departmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
