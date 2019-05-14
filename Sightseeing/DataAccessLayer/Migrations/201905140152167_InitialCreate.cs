namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        refActivityId = c.Int(nullable: false),
                        ActivityName = c.String(nullable: false, maxLength: 50),
                        ActivityDescryption = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Categories", t => t.refActivityId, cascadeDelete: true)
                .Index(t => t.refActivityId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ListActivities",
                c => new
                    {
                        ListActivityId = c.Int(nullable: false, identity: true),
                        refActivityId = c.Int(nullable: false),
                        refToDoListId = c.Int(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ListActivityId)
                .ForeignKey("dbo.Activities", t => t.refActivityId, cascadeDelete: true)
                .ForeignKey("dbo.ToDoLists", t => t.refToDoListId, cascadeDelete: true)
                .Index(t => t.refActivityId)
                .Index(t => t.refToDoListId);
            
            CreateTable(
                "dbo.ActivityExtensions",
                c => new
                    {
                        ActivityExtensionId = c.Int(nullable: false),
                        ActivityExtensionArticle = c.String(),
                        ActivityExtensionUrl = c.String(),
                    })
                .PrimaryKey(t => t.ActivityExtensionId)
                .ForeignKey("dbo.ListActivities", t => t.ActivityExtensionId)
                .Index(t => t.ActivityExtensionId);
            
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        ToDoListId = c.Int(nullable: false, identity: true),
                        refUserId = c.String(nullable: false, maxLength: 128),
                        ToDoListLikeCount = c.Int(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        ToDoListName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ToDoListId)
                .ForeignKey("dbo.AspNetUsers", t => t.refUserId, cascadeDelete: true)
                .Index(t => t.refUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserNickname = c.String(),
                        UserGender = c.String(),
                        UserBirthDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ListActivities", "refToDoListId", "dbo.ToDoLists");
            DropForeignKey("dbo.ToDoLists", "refUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ActivityExtensions", "ActivityExtensionId", "dbo.ListActivities");
            DropForeignKey("dbo.ListActivities", "refActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "refActivityId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ToDoLists", new[] { "refUserId" });
            DropIndex("dbo.ActivityExtensions", new[] { "ActivityExtensionId" });
            DropIndex("dbo.ListActivities", new[] { "refToDoListId" });
            DropIndex("dbo.ListActivities", new[] { "refActivityId" });
            DropIndex("dbo.Activities", new[] { "refActivityId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ToDoLists");
            DropTable("dbo.ActivityExtensions");
            DropTable("dbo.ListActivities");
            DropTable("dbo.Categories");
            DropTable("dbo.Activities");
        }
    }
}
