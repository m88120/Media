namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ismembeshipcategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Item", "CategoryTypeID", "CategoryType");
            DropForeignKey("Item", "CategoryID", "Category");
            DropIndex("Item", new[] { "CategoryTypeID" });
            DropIndex("Item", new[] { "CategoryID" });
            AddColumn("CategoryType", "IsMembershipCategory", c => c.Boolean(nullable: false));
            DropColumn("Item", "CategoryTypeID");
            DropColumn("Item", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("Item", "CategoryID", c => c.Long(nullable: false));
            AddColumn("Item", "CategoryTypeID", c => c.Long(nullable: false));
            DropColumn("CategoryType", "IsMembershipCategory");
            CreateIndex("Item", "CategoryID");
            CreateIndex("Item", "CategoryTypeID");
            AddForeignKey("Item", "CategoryID", "Category", "ID");
            AddForeignKey("Item", "CategoryTypeID", "CategoryType", "ID");
        }
    }
}
