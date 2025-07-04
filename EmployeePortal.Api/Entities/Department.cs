using EmployeePortal.Api.Common.Interfaces;

namespace EmployeePortal.Api.Entities
{
    public class Department: IEntityWithUUID
    {
        public Guid Uuid { get; set; }

        public string Name { get; set; }
    }
}
