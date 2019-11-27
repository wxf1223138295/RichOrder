using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Rich.Common.Base.IocObject
{
    /// <summary>
    /// 此工程基于autofac无法替换
    /// 对于Net依赖注入框架
    /// autofac功能是最强大的
    /// </summary>
    public interface IIocManager:IDisposable
    {
        IContainer iContainer { get; }
        ContainerBuilder BuilderContainer { get; }

        void SetContainer(IContainer _container);

    }
}
