using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Rich.Common.Base.AccessorDependencyInjection
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T Value { get; set; }

        public ObjectAccessor()
        {

        }

        public ObjectAccessor([CanBeNull] T obj)
        {
            Value = obj;
        }
    }
}
