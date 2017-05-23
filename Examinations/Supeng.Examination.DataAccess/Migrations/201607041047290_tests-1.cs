namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tests", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tests", "Name", c => c.String(maxLength: 100));
        }
    }
}
