using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Rich.Common.Base.AccessorDependencyInjection
{
    public interface IObjectAccessor<out T>
    {
        [CanBeNull]
        T Value { get; }
    }
}
