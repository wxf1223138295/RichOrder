using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Application.UserAppService
{
    public class LoginViewOutput
    {
        public string Code { get; set; }

        public VueLoginData Data { get; set; }
    }

    public class VueLoginData
    {
        public List<string> Roles { get; set; }

        public string Introduction { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public List<PagePermissionViewModel> RoleRouters { get; set; }
    }
}
