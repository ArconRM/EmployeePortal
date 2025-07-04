using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeePortalDbContext _context;

        public EmployeeRepository(EmployeePortalDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken token)
        {
            DbSet<Employee> set= _context.Set<Employee>();

            await set.AddAsync(employee, token);
            await _context.SaveChangesAsync(token);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            return await set
                .AsNoTracking()
                .Include(e => e.Department)
                .ToListAsync(token);
        }

        public async Task<Employee> GetEmployeeAsync(Guid uuid, CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            return await set
                .AsNoTracking()
                .Include(e => e.Department)
                .Where(e => e.Uuid == uuid)
                .FirstOrDefaultAsync(token);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            set.Update(employee);
            await _context.SaveChangesAsync(token);

            return employee;
        }

        public async Task DeleteEmployeeAsync(Guid uuid, CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            Employee employee = new()
            {
                Uuid = uuid
            };

            set.Remove(employee);
            await _context.SaveChangesAsync(token);
        }
    }
}
