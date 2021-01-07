using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Data.EF.Extensions;
using QDMarketPlace.Data.Entities;

namespace QDMarketPlace.Data.EF.Configurations
{
    public class AdvertistmentPositionConfiguration : DbEntityConfiguration<AdvertistmentPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPosition> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(20).IsRequired();
            entity.Property(c => c.PageId).HasMaxLength(20).IsRequired();
            // etc.
        }
    }
}
