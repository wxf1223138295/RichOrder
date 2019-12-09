using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rich.Order.Application.UserAppService
{
    public class RegisterViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string passWord { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public List<string> roleId { get; set; }
    }
}
