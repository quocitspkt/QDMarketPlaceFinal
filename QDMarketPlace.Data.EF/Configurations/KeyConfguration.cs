using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QDMarketPlace.Data.EF.Extensions;
using QDMarketPlace.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Data.EF.Configurations
{
    public class KeyConfiguration : DbEntityConfiguration<Key>
    {
        public override void Configure(EntityTypeBuilder<Key> entity)
        {
            entity.Property(c=>c.Value).HasMaxLength(200);
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ProductId).IsRequired();
        }
    }
}
