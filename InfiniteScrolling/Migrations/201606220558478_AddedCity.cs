namespace InfiniteScrolling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "City");
        }
    }
}
