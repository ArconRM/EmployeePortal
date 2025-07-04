using EmployeePortal.Api.Entities;

namespace EmployeePortal.Api.Common.DTO
{
    public class EmployeeDisplayDTO
    {
        public Guid Uuid { get; set; }

        public Department Department { get; set; }

        public string FullName { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public decimal Salary { get; set; }
    }
}
