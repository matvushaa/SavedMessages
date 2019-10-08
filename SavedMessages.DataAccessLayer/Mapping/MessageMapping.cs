using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Mapping
{
    public static class MessageMapping
    {
        public static void Mapping(EntityTypeConfiguration<Message> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasKey(k => k.MessageId);
            entityTypeConfiguration.Property(p => p.MessageText).HasMaxLength(900).HasColumnName("MessageText").HasColumnType("nvarchar").IsOptional();
            entityTypeConfiguration.Property(p => p.IsSaved);
            entityTypeConfiguration.Property(p => p.Time).HasColumnName("Time");
        }
    }
}
