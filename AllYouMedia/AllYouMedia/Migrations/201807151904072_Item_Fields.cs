namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("Item", "ContentURL", c => c.String(unicode: false));
            AddColumn("Item", "IsStockApplicable", c => c.Boolean(nullable: false));
            AddColumn("Item", "StockQty", c => c.Int(nullable: false));
            AddColumn("Item", "MinPurchaseQty", c => c.Int(nullable: false));
            AddColumn("Item", "MaxPurchaseQty", c => c.Int(nullable: false));
            AddColumn("Item", "ShortDescription", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AddColumn("Item", "LongDescription", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("Item", "LongDescription");
            DropColumn("Item", "ShortDescription");
            DropColumn("Item", "MaxPurchaseQty");
            DropColumn("Item", "MinPurchaseQty");
            DropColumn("Item", "StockQty");
            DropColumn("Item", "IsStockApplicable");
            DropColumn("Item", "ContentURL");
        }
    }
}
