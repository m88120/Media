namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genderspecfic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GenderSpecific",
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
                "GenderSpecificCategory",
                c => new
                    {
                        CategoryId = c.Long(nullable: false),
                        GenderSpecificId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.GenderSpecificId })
                .ForeignKey("Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("GenderSpecific", t => t.GenderSpecificId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.GenderSpecificId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("GenderSpecificCategory", "GenderSpecificId", "GenderSpecific");
            DropForeignKey("GenderSpecificCategory", "CategoryId", "Category");
            DropIndex("GenderSpecificCategory", new[] { "GenderSpecificId" });
            DropIndex("GenderSpecificCategory", new[] { "CategoryId" });
            DropTable("GenderSpecificCategory");
            DropTable("GenderSpecific");
        }
    }
}
