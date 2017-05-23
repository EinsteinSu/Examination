namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertests2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTests", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTests", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
