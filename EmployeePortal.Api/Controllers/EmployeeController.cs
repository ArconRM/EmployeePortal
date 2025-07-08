using AutoMapper;
using EmployeePortal.Api.Common.DTO;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost(nameof(CreateEmployee))]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] EmployeeCreateDTO employeeDTO,
            CancellationToken token)
        {
            try
            {
                Employee employee = _mapper.Map<EmployeeCreateDTO, Employee>(employeeDTO);
                Employee newEmployee = await _employeeService.CreateAsync(employee, token);
                EmployeeCreateDTO newEmployeeDTO = _mapper.Map<Employee, EmployeeCreateDTO>(newEmployee);
                return Ok(newEmployeeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet(nameof(GetAllEmployees))]
        public async Task<IActionResult> GetAllEmployees(
            int pageNumber,
            int pageSize,
            CancellationToken token)
        {
            try
            {
                IEnumerable<Employee> employees = await _employeeService.GetAllPaginatedAsync(
                    pageNumber,
                    pageSize,
                    token);

                IEnumerable<EmployeeDisplayDTO> employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDisplayDTO>>(employees);
                return Ok(employeesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet(nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee(
            Guid id,
            CancellationToken token)
        {

            try
            {
                var employee = await _employeeService.GetAsync(id, token);
                var employeeDto = _mapper.Map<Employee, EmployeeDisplayDTO>(employee);
                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
