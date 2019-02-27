namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RemoveProductionTbl : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("Production", "AspNetUserID", "AspNetUsers");
            //DropForeignKey("Item", "ProductionID", "Production");
            //DropIndex("Item", new[] { "ProductionID" });
            //DropIndex("Production", new[] { "AspNetUserID" });
            //AddColumn("AspNetUserRoles", "IsActive", c => c.Boolean(nullable: false));
            //AddColumn("AspNetUserRoles", "CreatedOn", c => c.DateTime(nullable: false, precision: 0));
            //AddColumn("AspNetUserRoles", "ModifiedOn", c => c.DateTime(precision: 0));
            //AddColumn("Item", "AspNetUserID", c => c.Long(nullable: false));
            //CreateIndex("Item", "AspNetUserID");
            AddForeignKey("Item", "AspNetUserID", "AspNetUsers", "Id");
            DropColumn("Item", "ProductionID");
            DropTable("Production");
        }

        public override void Down()
        {
            CreateTable(
                "Production",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200, storeType: "nvarchar"),
                        AspNetUserID = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("Item", "ProductionID", c => c.Long(nullable: false));
            DropForeignKey("Item", "AspNetUserID", "AspNetUsers");
            DropIndex("Item", new[] { "AspNetUserID" });
            DropColumn("Item", "AspNetUserID");
            DropColumn("AspNetUserRoles", "ModifiedOn");
            DropColumn("AspNetUserRoles", "CreatedOn");
            DropColumn("AspNetUserRoles", "IsActive");
            CreateIndex("Production", "AspNetUserID");
            CreateIndex("Item", "ProductionID");
            AddForeignKey("Item", "ProductionID", "Production", "ID");
            AddForeignKey("Production", "AspNetUserID", "AspNetUsers", "Id");
        }
    }
}
