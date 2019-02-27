namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FanModule : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "FanSharingUserRequest",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            AspNetUserID = c.Long(nullable: false),
            //            RequestingAspNetUserID = c.Long(nullable: false),
            //            IsGranted = c.Boolean(nullable: false),
            //            GrantedOn = c.DateTime(precision: 0),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
            //    //.ForeignKey("AspNetUsers", t => t.RequestingAspNetUserID)
            //    .Index(t => t.AspNetUserID)
            //    .Index(t => t.RequestingAspNetUserID);
            //AddForeignKey("FanSharingUserRequest", "AspNetUserID", "AspNetUsers", "Id");
            //AddForeignKey("FanSharingUserRequest", "RequestingAspNetUserID", "AspNetUsers", "Id");

            //CreateTable(
            //    "UserSpotlight",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            AspNetUserID = c.Long(nullable: false),
            //            ReviewingAspNetUserID = c.Long(nullable: false),
            //            Spotlight = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
            //    //.ForeignKey("AspNetUsers", t => t.ReviewingAspNetUserID)
            //    .Index(t => t.AspNetUserID)
            //    .Index(t => t.ReviewingAspNetUserID);
            //AddForeignKey("UserSpotlight", "AspNetUserID", "AspNetUsers", "Id");
            //AddForeignKey("UserSpotlight", "ReviewingAspNetUserID", "AspNetUsers", "Id");
            //CreateTable(
            //    "FanSharingFanRequest",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            AspNetUserID = c.Long(nullable: false),
            //            FanID = c.Long(nullable: false),
            //            IsGranted = c.Boolean(nullable: false),
            //            GrantedOn = c.DateTime(precision: 0),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
            //    //.ForeignKey("Fan", t => t.FanID)
            //    .Index(t => t.AspNetUserID)
            //    .Index(t => t.FanID);
            //AddForeignKey("FanSharingFanRequest", "AspNetUserID", "AspNetUsers", "Id");
            //AddForeignKey("FanSharingFanRequest", "FanID", "Fan", "ID");
            //CreateTable(
            //    "Fan",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            Email = c.String(maxLength: 200, storeType: "nvarchar"),
            //            Name = c.String(maxLength: 200, storeType: "nvarchar"),
            //            IsEmailConfirmed = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID);

            //CreateTable(
            //    "FanUserMap",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            AspNetUserID = c.Long(nullable: false),
            //            FanID = c.Long(nullable: false),
            //            Rating = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
            //    //.ForeignKey("Fan", t => t.FanID)
            //    .Index(t => t.AspNetUserID)
            //    .Index(t => t.FanID);
            //AddForeignKey("FanSharingFanRequest", "AspNetUserID", "AspNetUsers", "Id");
            //AddForeignKey("FanSharingFanRequest", "FanID", "Fan", "ID");
        }

        public override void Down()
        {
            DropForeignKey("FanSharingFanRequest", "FanID", "Fan");
            DropForeignKey("FanUserMap", "FanID", "Fan");
            DropForeignKey("FanUserMap", "AspNetUserID", "AspNetUsers");
            DropForeignKey("FanSharingFanRequest", "AspNetUserID", "AspNetUsers");
            DropForeignKey("UserSpotlight", "ReviewingAspNetUserID", "AspNetUsers");
            DropForeignKey("UserSpotlight", "AspNetUserID", "AspNetUsers");
            DropForeignKey("FanSharingUserRequest", "RequestingAspNetUserID", "AspNetUsers");
            DropForeignKey("FanSharingUserRequest", "AspNetUserID", "AspNetUsers");
            DropIndex("FanUserMap", new[] { "FanID" });
            DropIndex("FanUserMap", new[] { "AspNetUserID" });
            DropIndex("FanSharingFanRequest", new[] { "FanID" });
            DropIndex("FanSharingFanRequest", new[] { "AspNetUserID" });
            DropIndex("UserSpotlight", new[] { "ReviewingAspNetUserID" });
            DropIndex("UserSpotlight", new[] { "AspNetUserID" });
            DropIndex("FanSharingUserRequest", new[] { "RequestingAspNetUserID" });
            DropIndex("FanSharingUserRequest", new[] { "AspNetUserID" });
            DropTable("FanUserMap");
            DropTable("Fan");
            DropTable("FanSharingFanRequest");
            DropTable("UserSpotlight");
            DropTable("FanSharingUserRequest");
        }
    }
}
