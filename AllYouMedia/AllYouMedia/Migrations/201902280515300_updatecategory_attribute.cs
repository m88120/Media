namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory_attribute : DbMigration
    {
        public override void Up()
        {
            //DropIndex("UserCategoryMap", new[] { "AttributeID" });
            AlterColumn("UserCategoryMap", "AttributeID", c => c.Long());
            CreateIndex("UserCategoryMap", "AttributeID");
        }
        
        public override void Down()
        {
           // DropIndex("UserCategoryMap", new[] { "AttributeID" });
            AlterColumn("UserCategoryMap", "AttributeID", c => c.Long(nullable: false));
            CreateIndex("UserCategoryMap", "AttributeID");
        }
    }
}
