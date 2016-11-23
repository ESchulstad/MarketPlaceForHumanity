namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharityPosts", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ProviderPosts", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.TransportPosts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CharityPosts", "User_Id");
            CreateIndex("dbo.ProviderPosts", "User_Id");
            CreateIndex("dbo.TransportPosts", "User_Id");
            AddForeignKey("dbo.ProviderPosts", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TransportPosts", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CharityPosts", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharityPosts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransportPosts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProviderPosts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TransportPosts", new[] { "User_Id" });
            DropIndex("dbo.ProviderPosts", new[] { "User_Id" });
            DropIndex("dbo.CharityPosts", new[] { "User_Id" });
            DropColumn("dbo.TransportPosts", "User_Id");
            DropColumn("dbo.ProviderPosts", "User_Id");
            DropColumn("dbo.CharityPosts", "User_Id");
        }
    }
}
