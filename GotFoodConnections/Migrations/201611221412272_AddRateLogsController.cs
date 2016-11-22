namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRateLogsController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RateLogs",
                c => new
                    {
                        RateLogID = c.Int(nullable: false, identity: true),
                        VoteForID = c.Int(nullable: false),
                        UserName = c.String(),
                        Rate = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RateLogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RateLogs");
        }
    }
}
