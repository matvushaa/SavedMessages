using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Mapping
{
    public static class StickersMapping
    {
        public static void Mapping(EntityTypeConfiguration<Sticker> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasKey(k => k.StickerId);
            entityTypeConfiguration.Property(p => p.Stickers).HasColumnName("Stickers");
            entityTypeConfiguration.Property(p => p.Title).IsOptional();
            entityTypeConfiguration.Property(p => p.UserId).IsRequired();
        }
    }
}
