namespace Customers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegionDeleteModelMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Region");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Region", c => c.String(nullable: false));
        }
    }
}
