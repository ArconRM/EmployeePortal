using AutoMapper;
using EmployeePortal.Api.Common.DTO;
using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();

            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDisplayDTO>().ReverseMap();
        }
    }
}
