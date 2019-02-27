namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UCM_CatTypeID : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserCategoryMap", "CategoryTypeID", c => c.Long(nullable: false));
            CreateIndex("UserCategoryMap", "CategoryTypeID");
            AddForeignKey("UserCategoryMap", "CategoryTypeID", "CategoryType", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("UserCategoryMap", "CategoryTypeID", "CategoryType");
            DropIndex("UserCategoryMap", new[] { "CategoryTypeID" });
            DropColumn("UserCategoryMap", "CategoryTypeID");
        }
    }
}
