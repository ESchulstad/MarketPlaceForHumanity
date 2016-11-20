namespace GotFoodConnections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharityPosts",
                c => new
                    {
                        CharityPostID = c.Int(nullable: false, identity: true),
                        CharityID = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        FoodRequested = c.String(nullable: false),
                        WeightRequested = c.Double(nullable: false),
                        PeopleToFeed = c.Int(nullable: false),
                        DateRequested = c.DateTime(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.CharityPostID)
                .ForeignKey("dbo.CharityProfiles", t => t.CharityID, cascadeDelete: true)
                .Index(t => t.CharityID);
            
            CreateTable(
                "dbo.MainFeeds",
                c => new
                    {
                        MainFeedID = c.Int(nullable: false, identity: true),
                        ProviderPostID = c.Int(nullable: false),
                        CharityPostID = c.Int(nullable: false),
                        TransportPostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainFeedID)
                .ForeignKey("dbo.CharityPosts", t => t.CharityPostID, cascadeDelete: true)
                .ForeignKey("dbo.ProviderPosts", t => t.ProviderPostID, cascadeDelete: true)
                .ForeignKey("dbo.TransportPosts", t => t.TransportPostID, cascadeDelete: true)
                .Index(t => t.ProviderPostID)
                .Index(t => t.CharityPostID)
                .Index(t => t.TransportPostID);
            
            CreateTable(
                "dbo.ProviderPosts",
                c => new
                    {
                        ProviderPostID = c.Int(nullable: false, identity: true),
                        ProviderID = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        FoodType = c.String(nullable: false),
                        PeopleFed = c.String(),
                        PotentialAllergens = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        SpecialTransport = c.String(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.ProviderPostID)
                .ForeignKey("dbo.Providers", t => t.ProviderID, cascadeDelete: true)
                .Index(t => t.ProviderID);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderID = c.Int(nullable: false, identity: true),
                        OrgName = c.String(),
                        ContactName = c.String(),
                        ContactEmail = c.String(nullable: false),
                        ContactPhone = c.String(),
                        StreetNumber = c.String(),
                        StreetName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Website = c.String(),
                        Foods = c.String(),
                        ProvideTransport = c.String(),
                        NumOfDonation = c.Int(nullable: false),
                        StarRating = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProviderID)
                .ForeignKey("dbo.ProviderTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.ProviderTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransportPosts",
                c => new
                    {
                        TransportPostID = c.Int(nullable: false, identity: true),
                        TransportID = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                        StartTimeAvailable = c.DateTime(nullable: false),
                        EndTimeAvailable = c.DateTime(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.TransportPostID)
                .ForeignKey("dbo.Transports", t => t.TransportID, cascadeDelete: true)
                .Index(t => t.TransportID);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        TransportID = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(),
                        ContactName = c.String(nullable: false),
                        ContactPosition = c.String(),
                        ContactPhone = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        AdditionalContactInfo = c.String(),
                        StreetNumber = c.String(),
                        StreetName = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        TransportationTypes = c.String(nullable: false),
                        FoodTypes = c.String(nullable: false),
                        GeneralAvailability = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.TransportID);
            
            CreateTable(
                "dbo.CharityProfiles",
                c => new
                    {
                        CharityID = c.Int(nullable: false, identity: true),
                        CharityName = c.String(nullable: false),
                        StreetNumber = c.String(),
                        StreetName = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        MissionStatement = c.String(),
                        ContactName = c.String(),
                        ContactPosition = c.String(),
                        ContactPhone = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        AdditionalContactInfo = c.String(),
                        GenFoodRequest = c.String(),
                        ProvideTransport = c.String(),
                        CharityNum = c.String(nullable: false),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.CharityID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CharityPosts", "CharityID", "dbo.CharityProfiles");
            DropForeignKey("dbo.MainFeeds", "TransportPostID", "dbo.TransportPosts");
            DropForeignKey("dbo.TransportPosts", "TransportID", "dbo.Transports");
            DropForeignKey("dbo.MainFeeds", "ProviderPostID", "dbo.ProviderPosts");
            DropForeignKey("dbo.ProviderPosts", "ProviderID", "dbo.Providers");
            DropForeignKey("dbo.Providers", "TypeID", "dbo.ProviderTypes");
            DropForeignKey("dbo.MainFeeds", "CharityPostID", "dbo.CharityPosts");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TransportPosts", new[] { "TransportID" });
            DropIndex("dbo.Providers", new[] { "TypeID" });
            DropIndex("dbo.ProviderPosts", new[] { "ProviderID" });
            DropIndex("dbo.MainFeeds", new[] { "TransportPostID" });
            DropIndex("dbo.MainFeeds", new[] { "CharityPostID" });
            DropIndex("dbo.MainFeeds", new[] { "ProviderPostID" });
            DropIndex("dbo.CharityPosts", new[] { "CharityID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CharityProfiles");
            DropTable("dbo.Transports");
            DropTable("dbo.TransportPosts");
            DropTable("dbo.ProviderTypes");
            DropTable("dbo.Providers");
            DropTable("dbo.ProviderPosts");
            DropTable("dbo.MainFeeds");
            DropTable("dbo.CharityPosts");
        }
    }
}
