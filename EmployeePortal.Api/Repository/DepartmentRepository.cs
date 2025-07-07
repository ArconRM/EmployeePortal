using EmployeePortal.Api.Core;
using EmployeePortal.Api.Entities;
using EmployeePortal.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly EmployeePortalDbContext _context;

        public DepartmentRepository(EmployeePortalDbContext context): base(context)
        {
            _context = context;
        }
    }
}
