using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rich.Order.Domain.SeedWork;

namespace Rich.Order.Domain.Permissions
{
    public interface IRichUserRepository: IRepository
    {
        Task<IEnumerable<PagePermission>> GetAllPermissionAsync();
        Task<IEnumerable<ManagerPage>> GetAllManagerPageAsync();
        Task<IEnumerable<PagePermission>> GetAllPermissionByRoleIdAsync(string roles);
    }
}
