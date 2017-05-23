namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class security1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecurityActions",
                c => new
                    {
                        SecurityActionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SecurityActionId);
            
            CreateTable(
                "dbo.SecurityRoleActions",
                c => new
                    {
                        SecurityRoleActionId = c.Int(nullable: false, identity: true),
                        SecurityRoleId = c.Int(nullable: false),
                        SecurityActionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SecurityRoleActionId)
                .ForeignKey("dbo.SecurityActions", t => t.SecurityActionId, cascadeDelete: true)
                .ForeignKey("dbo.SecurityRoles", t => t.SecurityRoleId, cascadeDelete: true)
                .Index(t => t.SecurityRoleId)
                .Index(t => t.SecurityActionId);
            
            CreateTable(
                "dbo.SecurityRoles",
                c => new
                    {
                        SecurityRoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SecurityRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecurityRoleActions", "SecurityRoleId", "dbo.SecurityRoles");
            DropForeignKey("dbo.SecurityRoleActions", "SecurityActionId", "dbo.SecurityActions");
            DropIndex("dbo.SecurityRoleActions", new[] { "SecurityActionId" });
            DropIndex("dbo.SecurityRoleActions", new[] { "SecurityRoleId" });
            DropTable("dbo.SecurityRoles");
            DropTable("dbo.SecurityRoleActions");
            DropTable("dbo.SecurityActions");
        }
    }
}
