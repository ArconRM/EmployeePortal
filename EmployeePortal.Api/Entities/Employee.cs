using EmployeePortal.Api.Common.Interfaces;

namespace EmployeePortal.Api.Entities
{
    public class Employee: IEntityWithUUID
    {
        public Guid Uuid { get; set ; }

        public Guid DepartmentUuid { get; set ; }
        public Department Department { get; set; }

        public string FullName { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public decimal Salary { get; set; }
    }
}
