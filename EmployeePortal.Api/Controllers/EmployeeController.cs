using AutoMapper;
using EmployeePortal.Api.Common;
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
            [FromBody] EmployeeCreateDTO employeeDto,
            CancellationToken token)
        {
            try
            {
                Employee employee = _mapper.Map<EmployeeCreateDTO, Employee>(employeeDto);
                Employee newEmployee = await _employeeService.CreateAsync(employee, token);
                EmployeeCreateDTO newEmployeeDto = _mapper.Map<Employee, EmployeeCreateDTO>(newEmployee);
                return Ok(newEmployeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
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
                PaginatedResult<Employee> employeesResult = await _employeeService.GetAllPaginatedAsync(
                    pageNumber,
                    pageSize,
                    token);

                IEnumerable<EmployeeDisplayDTO> employeesDto =
                    _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDisplayDTO>>(employeesResult.Items);

                var response = new
                {
                    items = employeesDto,
                    totalCount = employeesResult.TotalCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
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
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee(
            [FromBody] EmployeeUpdateDTO employeeDto,
            CancellationToken token)
        {
            try
            {
                Employee employee = _mapper.Map<EmployeeUpdateDTO, Employee>(employeeDto);
                Employee updatedEmployee = await _employeeService.UpdateAsync(employee, token);
                EmployeeUpdateDTO updatedEmployeeDto = _mapper.Map<Employee, EmployeeUpdateDTO>(updatedEmployee);
                return Ok(updatedEmployeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(
            Guid id,
            CancellationToken token)
        {
            try
            {
                await _employeeService.DeleteAsync(id, token);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}