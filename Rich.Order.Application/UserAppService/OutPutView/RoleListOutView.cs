using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Application.UserAppService
{
    public class RoleListOutView
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PagePermissionViewModel> Routes { get; set; }
    }
}
