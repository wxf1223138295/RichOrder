using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Rich.Common.Base.AccessorDependencyInjection;

namespace Rich.Common.Base.RichDapper
{
    public class DefaultDapperImp: DapperExtension
    {
        public DefaultDapperImp(IObjectAccessor<DapperOptions> objectAccessor) : base(objectAccessor?.Value.DefaultConnectStrName)
        {
        }
    }
}
