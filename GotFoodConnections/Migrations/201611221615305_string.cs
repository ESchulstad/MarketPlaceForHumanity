namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CharityPosts", "WeightRequested", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CharityPosts", "WeightRequested", c => c.Double(nullable: false));
        }
    }
}
