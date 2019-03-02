namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusecatrgory : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserCategoryMap", "GenreID", c => c.Long());
            AddColumn("UserCategoryMap", "InstrumentID", c => c.Long());
            AddColumn("UserCategoryMap", "InstrumentSpeciID", c => c.Long());
            AddColumn("UserCategoryMap", "InstrumentSpecification_ID", c => c.Long());
            AlterColumn("UserCategoryMap", "SubCategoryID", c => c.Long());
            CreateIndex("UserCategoryMap", "GenreID");
            CreateIndex("UserCategoryMap", "InstrumentID");
            CreateIndex("UserCategoryMap", "SubCategoryID");
            CreateIndex("UserCategoryMap", "InstrumentSpecification_ID");
            AddForeignKey("UserCategoryMap", "GenreID", "GenreCategory", "ID");
            AddForeignKey("UserCategoryMap", "InstrumentID", "InstrumentCategory", "ID");
            AddForeignKey("UserCategoryMap", "InstrumentSpecification_ID", "InstrumentSpecificationCategory", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("UserCategoryMap", "InstrumentSpecification_ID", "InstrumentSpecificationCategory");
            DropForeignKey("UserCategoryMap", "InstrumentID", "InstrumentCategory");
            DropForeignKey("UserCategoryMap", "GenreID", "GenreCategory");
            DropIndex("UserCategoryMap", new[] { "InstrumentSpecification_ID" });
            DropIndex("UserCategoryMap", new[] { "InstrumentID" });
            DropIndex("UserCategoryMap", new[] { "GenreID" });
            AlterColumn("UserCategoryMap", "SubCategoryID", c => c.Long(nullable: false));
            DropColumn("UserCategoryMap", "InstrumentSpecification_ID");
            DropColumn("UserCategoryMap", "InstrumentSpeciID");
            DropColumn("UserCategoryMap", "InstrumentID");
            DropColumn("UserCategoryMap", "GenreID");
            CreateIndex("UserCategoryMap", "SubCategoryID");
        }
    }
}
