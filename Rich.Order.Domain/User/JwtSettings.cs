using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Domain.User
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}
