using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Rich.Order.Domain.ServiceCollectionExtensions
{
    public class RichSwaggerOption
    {
        [NotNull]
        public string pathName { set; get; } 
        [NotNull]
        public string versionName { get; set; } 
        [NotNull]
        public string swaggerTitle { get; set; } 
    }
}
