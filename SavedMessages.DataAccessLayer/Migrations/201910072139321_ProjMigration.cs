namespace SavedMessages.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileLocations",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        File = c.Binary(),
                        UserId = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        IsSaved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        PassWord = c.String(maxLength: 300),
                        PermissionId = c.Int(nullable: false),
                        IsVerifyed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageText = c.String(maxLength: 900),
                        Time = c.DateTime(nullable: false),
                        IsSaved = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stickers",
                c => new
                    {
                        StickerId = c.Int(nullable: false, identity: true),
                        Stickers = c.Binary(),
                        Title = c.String(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StickerId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stickers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.FileLocations", "UserId", "dbo.Users");
            DropIndex("dbo.Stickers", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "PermissionId" });
            DropIndex("dbo.FileLocations", new[] { "UserId" });
            DropTable("dbo.Stickers");
            DropTable("dbo.Permissions");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.FileLocations");
        }
    }
}
