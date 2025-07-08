namespace EmployeePortal.Api.Common.DTO
{
    public class EmployeeQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Фильтры
        public string? Department { get; set; }

        public string? FullName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public DateOnly? EmploymentDate { get; set; }

        public decimal? Salary { get; set; }

        // Сортировка
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; } = "asc";
    }
}
