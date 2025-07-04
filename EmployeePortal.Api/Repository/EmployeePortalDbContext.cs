using EmployeePortal.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeePortal.Api.Repository
{
    public class EmployeePortalDbContext: DbContext
    {

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public EmployeePortalDbContext(DbContextOptions<EmployeePortalDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
