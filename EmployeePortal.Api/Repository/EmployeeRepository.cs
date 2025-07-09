using EmployeePortal.Api.Common;
using EmployeePortal.Api.Common.DTO;
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

        public async Task<PaginatedResult<Employee>> GetAllPaginatedAsync(
            EmployeeQueryParameters queryParameters,
            CancellationToken token)
        {
            IQueryable<Employee> query = _context.Set<Employee>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryParameters.FullName))
            {
                query = query.Where(e => e.FullName.Contains(queryParameters.FullName));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.Department))
            {
                query = query.Where(e => e.Department.Name.Contains(queryParameters.Department));
            }

            if (queryParameters.BirthDate.HasValue)
            {
                query = query.Where(e => e.BirthDate == queryParameters.BirthDate.Value);
            }

            if (queryParameters.EmploymentDate.HasValue)
            {
                query = query.Where(e => e.EmploymentDate == queryParameters.EmploymentDate.Value);
            }

            if (queryParameters.Salary.HasValue)
            {
                query = query.Where(e => e.Salary == queryParameters.Salary.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortColumn))
            {
                var isDescending = queryParameters.SortDirection?.ToLower() == "desc";

                query = (queryParameters.SortColumn.ToLower()) switch
                {
                    "department" => isDescending
                        ? query.OrderByDescending(e => e.Department.Name)
                        : query.OrderBy(e => e.Department.Name),
                    "birthdate" => isDescending
                        ? query.OrderByDescending(e => e.BirthDate)
                        : query.OrderBy(e => e.BirthDate),
                    "employmentdate" => isDescending
                        ? query.OrderByDescending(e => e.EmploymentDate)
                        : query.OrderBy(e => e.EmploymentDate),
                    "salary" => isDescending
                        ? query.OrderByDescending(e => e.Salary)
                        : query.OrderBy(e => e.Salary),
                    _ => isDescending
                        ? query.OrderByDescending(e => e.FullName)
                        : query.OrderBy(e => e.FullName)
                };
            }
            else
            {
                query = query.OrderBy(e => e.FullName);
            }

            int totalCount = await query.CountAsync(token);

            var items = await query
                .Include(e => e.Department)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync(token);

            return new PaginatedResult<Employee> { Items = items, TotalCount = totalCount };
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