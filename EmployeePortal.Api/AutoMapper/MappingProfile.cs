using AutoMapper;
using EmployeePortal.Api.Common.DTO;
using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDisplayDTO>().ReverseMap();
        }
    }
}
