namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingAndRateLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Providers", "Rating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Providers", "Rating");
        }
    }
}
