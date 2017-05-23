namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Description");
        }
    }
}
