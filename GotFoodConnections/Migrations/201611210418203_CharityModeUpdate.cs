namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharityModeUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharityProfiles", "CharityProfile_CharityID", c => c.Int());
            CreateIndex("dbo.CharityProfiles", "CharityProfile_CharityID");
            AddForeignKey("dbo.CharityProfiles", "CharityProfile_CharityID", "dbo.CharityProfiles", "CharityID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharityProfiles", "CharityProfile_CharityID", "dbo.CharityProfiles");
            DropIndex("dbo.CharityProfiles", new[] { "CharityProfile_CharityID" });
            DropColumn("dbo.CharityProfiles", "CharityProfile_CharityID");
        }
    }
}
