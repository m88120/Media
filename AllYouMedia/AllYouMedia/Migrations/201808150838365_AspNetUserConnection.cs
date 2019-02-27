namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AspNetUserConnection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AspNetUserConnection",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AspNetUserID = c.Long(nullable: false),
                        ConnectedAspNetUserID = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.AspNetUserID)
                .Index(t => t.ConnectedAspNetUserID);
            AddForeignKey("AspNetUserConnection", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("AspNetUserConnection", "ConnectedAspNetUserID", "AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("AspNetUserConnection", "ConnectedAspNetUserID", "AspNetUsers");
            DropForeignKey("AspNetUserConnection", "AspNetUserID", "AspNetUsers");
            DropIndex("AspNetUserConnection", new[] { "ConnectedAspNetUserID" });
            DropIndex("AspNetUserConnection", new[] { "AspNetUserID" });
            DropTable("AspNetUserConnection");
        }
    }
}
