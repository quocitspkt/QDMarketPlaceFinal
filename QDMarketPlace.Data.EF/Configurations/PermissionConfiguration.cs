using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Data.EF.Extensions;
using QDMarketPlace.Data.Entities;

namespace QDMarketPlace.Data.EF.Configurations
{
    public class PermissionConfiguration : DbEntityConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.FunctionId).IsRequired()
                .HasMaxLength(128).IsUnicode(false);
            // etc.
        }
    }
}
