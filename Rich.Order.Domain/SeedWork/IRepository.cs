using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Domain.SeedWork
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
