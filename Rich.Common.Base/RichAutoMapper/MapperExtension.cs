using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Rich.Common.Base.RichAutoMapper
{
    public static class MapperExtension
    {
        public static void AddRichAutoMapper(this IServiceCollection service,Action<IServiceProvider,IMapperConfigurationExpression> action,List<Type> types)
        {
            service.AddAutoMapper(action, types);
        }
    }
}
