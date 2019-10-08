using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Mapping
{
    public static class PermissionMaping
    {
        public static void Mapping(EntityTypeConfiguration<Permissions> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasKey(k => k.Id);
            entityTypeConfiguration.Property(p => p.PermissionName).HasMaxLength(200).HasColumnName("PermissionName").HasColumnType("nvarchar");
            entityTypeConfiguration.HasMany(p => p.User);
        }
    }
}
