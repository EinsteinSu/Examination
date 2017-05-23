namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestarttimecolumnname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "StartTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tests", "StarTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "StarTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tests", "StartTime");
        }
    }
}
