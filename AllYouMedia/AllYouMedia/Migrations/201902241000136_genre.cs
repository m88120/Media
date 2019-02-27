namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GenreCategory",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GenderSpecificGenerCategory",
                c => new
                    {
                        GenderSpecificId = c.Long(nullable: false),
                        GenerCategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenderSpecificId, t.GenerCategoryId })
                .ForeignKey("GenderSpecific", t => t.GenderSpecificId, cascadeDelete: true)
                .ForeignKey("GenreCategory", t => t.GenerCategoryId, cascadeDelete: true)
                .Index(t => t.GenderSpecificId)
                .Index(t => t.GenerCategoryId);
            
            CreateTable(
                "CategoryGener",
                c => new
                    {
                        CategoryId = c.Long(nullable: false),
                        GenerCategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.GenerCategoryId })
                .ForeignKey("Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("GenreCategory", t => t.GenerCategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.GenerCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("CategoryGener", "GenerCategoryId", "GenreCategory");
            DropForeignKey("CategoryGener", "CategoryId", "Category");
            DropForeignKey("GenderSpecificGenerCategory", "GenerCategoryId", "GenreCategory");
            DropForeignKey("GenderSpecificGenerCategory", "GenderSpecificId", "GenderSpecific");
            DropIndex("CategoryGener", new[] { "GenerCategoryId" });
            DropIndex("CategoryGener", new[] { "CategoryId" });
            DropIndex("GenderSpecificGenerCategory", new[] { "GenerCategoryId" });
            DropIndex("GenderSpecificGenerCategory", new[] { "GenderSpecificId" });
            DropTable("CategoryGener");
            DropTable("GenderSpecificGenerCategory");
            DropTable("GenreCategory");
        }
    }
}
