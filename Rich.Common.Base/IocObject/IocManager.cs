using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Rich.Common.Base.IocObject
{
    public class IocManager: IIocManager
    { 
        /// <summary>
        /// 单例
        /// </summary>
        public static IocManager Instance { get; private set; }

        public ContainerBuilder BuilderContainer { get; private set; }
        public void SetContainer(IContainer _container)
        {
            iContainer = _container;
        }

        static IocManager()
        {
            Instance = new IocManager();
        }
      
        public IContainer iContainer { get; private set; }
        public IocManager()
        {
            BuilderContainer = new ContainerBuilder();

            //Register self!
            BuilderContainer
                .RegisterInstance(this)
                .As<IIocManager>();
        }
        public void Dispose()
        {
            iContainer.Dispose();
        }


    }
}
