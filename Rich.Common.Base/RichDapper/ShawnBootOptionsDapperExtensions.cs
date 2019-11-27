using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using Rich.Common.Base.AccessorDependencyInjection;
using Rich.Common.Base.Options;

namespace Rich.Common.Base.RichDapper
{
    public static class ShawnBootOptionsDapperExtensions
    {
        public static ShawnBootOptions UseDapper(this ShawnBootOptions options,[NotNull] Action<DapperOptions> dapperoptions)
        {
            DapperOptions obj=new DapperOptions();
            dapperoptions?.Invoke(obj);

            options._iServiceCollection.AddObjectAccessor<DapperOptions>(obj);

            options._IocManager.BuilderContainer
                .RegisterType<DefaultDapperImp>()
                .InstancePerLifetimeScope()
                .As<IDapperExtension>();

            return options;
        }
    }
}
