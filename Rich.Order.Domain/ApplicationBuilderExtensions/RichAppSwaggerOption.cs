using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Rich.Order.Domain.ApplicationBuilderExtensions
{
    public class RichAppSwaggerOption
    {
        [NotNull]
        public string urlName { get; set; }
        [NotNull]
        public string swaggerUIName { get; set; }
    }
}
