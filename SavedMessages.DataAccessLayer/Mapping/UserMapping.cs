using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Mapping
{
    public static class UserMaping
    {
        public static void Mapping(EntityTypeConfiguration<User> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasKey(k => k.Id);
            entityTypeConfiguration.Property(p => p.PermissionId);
            entityTypeConfiguration.Property(p => p.FirstName).HasMaxLength(20).HasColumnName("Name").HasColumnType("nvarchar");
            entityTypeConfiguration.Property(p => p.Email).HasMaxLength(100).HasColumnName("Email").HasColumnType("nvarchar");
            entityTypeConfiguration.Property(p => p.Password).HasMaxLength(300).HasColumnName("PassWord").HasColumnType("nvarchar");
            entityTypeConfiguration.HasMany(p => p.Messages);
            entityTypeConfiguration.HasMany(p => p.Files);
            entityTypeConfiguration.HasMany(p => p.Stickers);
        }
    }
}
