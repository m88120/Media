namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertest_category : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserCategoryMap", "GenderID", c => c.Long());
            CreateIndex("UserCategoryMap", "GenderID");
            AddForeignKey("UserCategoryMap", "GenderID", "GenderSpecific", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("UserCategoryMap", "GenderID", "GenderSpecific");
            DropIndex("UserCategoryMap", new[] { "GenderID" });
            DropColumn("UserCategoryMap", "GenderID");
        }
    }
}
