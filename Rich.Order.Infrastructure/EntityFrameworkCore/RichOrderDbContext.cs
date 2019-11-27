using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rich.Order.Domain.User;

namespace Rich.Order.Infrastructure.EntityFrameworkCore
{
    public class RichOrderDbContext: IdentityDbContext<RichOrderUser>
    {
        public RichOrderDbContext(DbContextOptions<RichOrderDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
