using EmployeePortal.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeePortal.Api.Repository.EntityConfiguration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Uuid);

            builder.Property(d => d.Uuid)
                .ValueGeneratedOnAdd();
        }
    }
}
