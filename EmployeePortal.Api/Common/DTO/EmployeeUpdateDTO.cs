namespace EmployeePortal.Api.Common.DTO
{
    public class EmployeeUpdateDTO
    {
        public Guid Uuid { get; set; }

        public Guid DepartmentUuid { get; set; }

        public string FullName { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public decimal Salary { get; set; }
    }
}
