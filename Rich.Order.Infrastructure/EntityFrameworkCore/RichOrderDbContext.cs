using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rich.Order.Domain.Permissions;
using Rich.Order.Domain.User;
using Rich.Order.Infrastructure.EntityConfiguration;

namespace Rich.Order.Infrastructure.EntityFrameworkCore
{
    public class RichOrderDbContext: IdentityDbContext<RichOrderUser,RichOrderRole,string>
    {
        public virtual DbSet<PagePermission> PagePermission { get; set; }
        public virtual DbSet<ManagerPage> ManagerPage { get; set; }
        public RichOrderDbContext(DbContextOptions<RichOrderDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PagePermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerPageEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
