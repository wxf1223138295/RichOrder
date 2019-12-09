using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rich.Order.Domain.Permissions;
using Rich.Order.Domain.SeedWork;
using Rich.Order.Infrastructure.EntityFrameworkCore;

namespace Rich.Order.Infrastructure.RichRepository
{
    public class RichUserRepository: IRichUserRepository
    {
        private readonly RichOrderDbContext _context;

        public RichUserRepository(RichOrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }


        public async Task<IEnumerable<PagePermission>> GetAllPermissionAsync()
        {
            var result= await _context.PagePermission.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ManagerPage>> GetAllManagerPageAsync()
        {
            var result = await _context.ManagerPage.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PagePermission>> GetAllPermissionByRoleIdAsync(string roles)
        {
            var result = await _context.PagePermission.Where(p=>p.RoleIds.Contains(roles)).AsNoTracking().ToListAsync();
            return result;
        }
    }
}
