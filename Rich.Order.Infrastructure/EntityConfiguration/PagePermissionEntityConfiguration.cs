using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rich.Order.Domain.Permissions;
using Rich.Order.Infrastructure.EntityFrameworkCore;

namespace Rich.Order.Infrastructure.EntityConfiguration
{
    public class PagePermissionEntityConfiguration : IEntityTypeConfiguration<PagePermission>
    {
        public void Configure(EntityTypeBuilder<PagePermission> builder)
        {
            builder.ToTable("PagePermission", DBSchemas.DefaultSchema);

            //设置主键
            builder.HasKey(p => p.Id);
        }
    }
}
