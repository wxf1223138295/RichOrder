using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rich.Common.Base.RichReturnModel;

namespace Rich.Order.Application.UserAppService
{
    public interface IRichUserAppService
    {
        Task<RichBizReturn<List<PagePermissionViewModel>>> GetRouterByRoleId(string roleId = "");
    }
}
