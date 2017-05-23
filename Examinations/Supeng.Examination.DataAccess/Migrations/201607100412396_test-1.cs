namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Generated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tests", "GenerateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "GenerateTime");
            DropColumn("dbo.Tests", "Generated");
        }
    }
}
