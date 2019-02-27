namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test_category : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserCategoryMap", "GenderID", c => c.Long(nullable: false));
            AddColumn("UserCategoryMap", "GenreID", c => c.Long(nullable: false));
            AddColumn("UserCategoryMap", "InstrumentID", c => c.Long(nullable: false));
            AddColumn("UserCategoryMap", "InstrumentSpeciID", c => c.Long(nullable: false));
            AddColumn("UserCategoryMap", "InstrumentSpecification_ID", c => c.Long());
            CreateIndex("UserCategoryMap", "GenderID");
            CreateIndex("UserCategoryMap", "GenreID");
            CreateIndex("UserCategoryMap", "InstrumentID");
            CreateIndex("UserCategoryMap", "InstrumentSpecification_ID");
            AddForeignKey("UserCategoryMap", "GenderID", "GenderSpecific", "ID");
            AddForeignKey("UserCategoryMap", "GenreID", "GenreCategory", "ID");
            AddForeignKey("UserCategoryMap", "InstrumentID", "InstrumentCategory", "ID");
            AddForeignKey("UserCategoryMap", "InstrumentSpecification_ID", "InstrumentSpecificationCategory", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("UserCategoryMap", "InstrumentSpecification_ID", "InstrumentSpecificationCategory");
            DropForeignKey("UserCategoryMap", "InstrumentID", "InstrumentCategory");
            DropForeignKey("UserCategoryMap", "GenreID", "GenreCategory");
            DropForeignKey("UserCategoryMap", "GenderID", "GenderSpecific");
            DropIndex("UserCategoryMap", new[] { "InstrumentSpecification_ID" });
            DropIndex("UserCategoryMap", new[] { "InstrumentID" });
            DropIndex("UserCategoryMap", new[] { "GenreID" });
            DropIndex("UserCategoryMap", new[] { "GenderID" });
            DropColumn("UserCategoryMap", "InstrumentSpecification_ID");
            DropColumn("UserCategoryMap", "InstrumentSpeciID");
            DropColumn("UserCategoryMap", "InstrumentID");
            DropColumn("UserCategoryMap", "GenreID");
            DropColumn("UserCategoryMap", "GenderID");
        }
    }
}
