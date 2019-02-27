namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserCategory : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("AspNetUsers", "SubCategoryID", "SubCategory");
            //DropIndex("AspNetUsers", new[] { "SubCategoryID" });
            //CreateTable(
            //    "UserCategoryMap",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            AspNetUserID = c.Long(nullable: false),
            //            CategoryID = c.Long(nullable: false),
            //            SubCategoryID = c.Long(nullable: false),
            //            AttributeID = c.Long(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
            //    //.ForeignKey("Attribute", t => t.AttributeID)
            //    //.ForeignKey("Category", t => t.CategoryID)
            //    //.ForeignKey("SubCategory", t => t.SubCategoryID)
            //    .Index(t => t.AspNetUserID)
            //    .Index(t => t.CategoryID)
            //    .Index(t => t.SubCategoryID)
            //    .Index(t => t.AttributeID);
            AddForeignKey("UserCategoryMap", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("UserCategoryMap", "CategoryID", "Category", "ID");
            AddForeignKey("UserCategoryMap", "SubCategoryID", "SubCategory", "ID");
            AddForeignKey("UserCategoryMap", "AttributeID", "Attribute", "ID");

            //AddColumn("Attribute", "SubCategoryID", c => c.Long(nullable: false));
            //AddColumn("SubCategory", "IsTalent", c => c.Boolean(nullable: false));
            //AddColumn("SubCategory", "IsProduction", c => c.Boolean(nullable: false));
            //AddColumn("Category", "IsTalent", c => c.Boolean(nullable: false));
            //AddColumn("Category", "IsProduction", c => c.Boolean(nullable: false));
            //AddColumn("CategoryType", "IsTalent", c => c.Boolean(nullable: false));
            //AddColumn("CategoryType", "IsProduction", c => c.Boolean(nullable: false));
            //CreateIndex("Attribute", "SubCategoryID");
            AddForeignKey("Attribute", "SubCategoryID", "SubCategory", "ID");
            DropColumn("AspNetUsers", "SubCategoryID");
        }

        public override void Down()
        {
            AddColumn("AspNetUsers", "SubCategoryID", c => c.Long(nullable: false));
            DropForeignKey("UserCategoryMap", "SubCategoryID", "SubCategory");
            DropForeignKey("UserCategoryMap", "CategoryID", "Category");
            DropForeignKey("UserCategoryMap", "AttributeID", "Attribute");
            DropForeignKey("UserCategoryMap", "AspNetUserID", "AspNetUsers");
            DropForeignKey("Attribute", "SubCategoryID", "SubCategory");
            DropIndex("UserCategoryMap", new[] { "AttributeID" });
            DropIndex("UserCategoryMap", new[] { "SubCategoryID" });
            DropIndex("UserCategoryMap", new[] { "CategoryID" });
            DropIndex("UserCategoryMap", new[] { "AspNetUserID" });
            DropIndex("Attribute", new[] { "SubCategoryID" });
            DropColumn("CategoryType", "IsProduction");
            DropColumn("CategoryType", "IsTalent");
            DropColumn("Category", "IsProduction");
            DropColumn("Category", "IsTalent");
            DropColumn("SubCategory", "IsProduction");
            DropColumn("SubCategory", "IsTalent");
            DropColumn("Attribute", "SubCategoryID");
            DropTable("UserCategoryMap");
            CreateIndex("AspNetUsers", "SubCategoryID");
            AddForeignKey("AspNetUsers", "SubCategoryID", "SubCategory", "ID");
        }
    }
}
