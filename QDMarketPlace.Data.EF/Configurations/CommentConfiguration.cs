using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QDMarketPlace.Data.EF.Extensions;
using QDMarketPlace.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Data.EF.Configurations
{
    public class CommentConfiguration : DbEntityConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.Property(c => c.Content).HasMaxLength(200).IsRequired().IsUnicode(true);
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ProductId).IsRequired();
        }
    }
}
