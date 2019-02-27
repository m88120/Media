namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Instrument_category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "InstrumentCategory",
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
                "InstrumentCategoryGener",
                c => new
                    {
                        GenreCategoryId = c.Long(nullable: false),
                        InstrumentCategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreCategoryId, t.InstrumentCategoryId })
                .ForeignKey("GenreCategory", t => t.GenreCategoryId, cascadeDelete: true)
                .ForeignKey("InstrumentCategory", t => t.InstrumentCategoryId, cascadeDelete: true)
                .Index(t => t.GenreCategoryId)
                .Index(t => t.InstrumentCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("InstrumentCategoryGener", "InstrumentCategoryId", "InstrumentCategory");
            DropForeignKey("InstrumentCategoryGener", "GenreCategoryId", "GenreCategory");
            DropIndex("InstrumentCategoryGener", new[] { "InstrumentCategoryId" });
            DropIndex("InstrumentCategoryGener", new[] { "GenreCategoryId" });
            DropTable("InstrumentCategoryGener");
            DropTable("InstrumentCategory");
        }
    }
}
