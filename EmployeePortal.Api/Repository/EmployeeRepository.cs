using EmployeePortal.Api.Core;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly EmployeePortalDbContext _context;

        public EmployeeRepository(EmployeePortalDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            return await set
                .AsNoTracking()
                .Include(e => e.Department)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            return await set.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.Department)
                .ToListAsync(token);
        }

        public override async Task<Employee> GetAsync(Guid uuid, CancellationToken token)
        {
            DbSet<Employee> set = _context.Set<Employee>();

            return await set
                .AsNoTracking()
                .Include(e => e.Department)
                .Where(e => e.Uuid == uuid)
                .FirstOrDefaultAsync(token);
        }
    }
}
