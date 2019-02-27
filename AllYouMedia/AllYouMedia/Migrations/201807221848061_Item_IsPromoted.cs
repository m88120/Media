namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_IsPromoted : DbMigration
    {
        public override void Up()
        {
            AddColumn("Item", "IsPromoted", c => c.Boolean(nullable: false));
            AddColumn("Item", "IsFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Item", "IsFeatured");
            DropColumn("Item", "IsPromoted");
        }
    }
}
