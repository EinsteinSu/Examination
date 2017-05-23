namespace Supeng.Examination.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_delete_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Deleted");
        }
    }
}
