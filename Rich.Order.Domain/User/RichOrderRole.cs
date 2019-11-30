using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Rich.Order.Domain.User
{
    public class RichOrderRole:IdentityRole
    {
        public string Avatar { get; set; }
        public string Introduction { get; set; }
        public string ShowName { get; set; }
        
    }
}
