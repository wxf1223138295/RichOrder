using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Rich.Order.Infrastructure.EntityFrameworkCore
{
    public class RichOrderDbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
