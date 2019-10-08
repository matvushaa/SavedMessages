using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Mapping
{
    public static class FileLocationMapping
    {
        public static void Mapping(EntityTypeConfiguration<FileLocation> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasKey(k => k.FileId);
            entityTypeConfiguration.Property(p => p.File).HasColumnName("File");
            entityTypeConfiguration.Property(p => p.IsSaved);
            entityTypeConfiguration.Property(p => p.Time).HasColumnName("Time");
        }
    }
}
