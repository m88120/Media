namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class instrumentSpecification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "InstrumentSpecificationCategory",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200, storeType: "nvarchar"),
                        InstrumentCategoryId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("InstrumentCategory", t => t.InstrumentCategoryId)
                .Index(t => t.InstrumentCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("InstrumentSpecificationCategory", "InstrumentCategoryId", "InstrumentCategory");
            DropIndex("InstrumentSpecificationCategory", new[] { "InstrumentCategoryId" });
            DropTable("InstrumentSpecificationCategory");
        }
    }
}
