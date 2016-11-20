namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Providers", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Transports", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.CharityProfiles", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Providers", "User_Id");
            CreateIndex("dbo.Transports", "User_Id");
            CreateIndex("dbo.CharityProfiles", "User_Id");
            AddForeignKey("dbo.Providers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transports", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CharityProfiles", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharityProfiles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Providers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CharityProfiles", new[] { "User_Id" });
            DropIndex("dbo.Transports", new[] { "User_Id" });
            DropIndex("dbo.Providers", new[] { "User_Id" });
            DropColumn("dbo.CharityProfiles", "User_Id");
            DropColumn("dbo.Transports", "User_Id");
            DropColumn("dbo.Providers", "User_Id");
        }
    }
}
