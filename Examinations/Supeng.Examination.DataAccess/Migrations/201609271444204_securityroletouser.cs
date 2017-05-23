namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityroletouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "SecurityRoleId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "SecurityRoleId");
            AddForeignKey("dbo.UserProfiles", "SecurityRoleId", "dbo.SecurityRoles", "SecurityRoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "SecurityRoleId", "dbo.SecurityRoles");
            DropIndex("dbo.UserProfiles", new[] { "SecurityRoleId" });
            DropColumn("dbo.UserProfiles", "SecurityRoleId");
        }
    }
}
