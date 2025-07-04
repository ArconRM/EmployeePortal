using EmployeePortal.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeePortal.Api.Repository.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Uuid);

            builder.Property(e => e.Uuid)
                .ValueGeneratedOnAdd();

            builder.
                HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentUuid);
        }
    }
}
