using SavedMessages.DataAccessLayer.Entities;
using SavedMessages.DataAccessLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        { }
        public ProjectContext(string connectionString)
            : base(connectionString)
        {

        }

        public virtual DbSet<Sticker> Stickers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<FileLocation> FileLocation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            FileLocationMapping.Mapping(modelBuilder.Entity<FileLocation>());
            StickersMapping.Mapping(modelBuilder.Entity<Sticker>());
            MessageMapping.Mapping(modelBuilder.Entity<Message>());
            PermissionMaping.Mapping(modelBuilder.Entity<Permissions>());
            UserMaping.Mapping(modelBuilder.Entity<User>());
        }
    }
}
