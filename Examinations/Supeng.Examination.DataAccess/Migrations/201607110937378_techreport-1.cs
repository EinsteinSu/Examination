namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class techreport1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TechReports",
                c => new
                    {
                        TechReportId = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TechReportId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechReports", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.TechReports", new[] { "UserId" });
            DropTable("dbo.TechReports");
        }
    }
}
