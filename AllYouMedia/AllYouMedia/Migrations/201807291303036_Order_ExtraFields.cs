namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_ExtraFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("Order", "NetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Order", "TotalDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Order", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Order", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Order", "PayableAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "BasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "SellPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "LineAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "LineDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("OrderItem", "ShippingDate", c => c.DateTime(precision: 0));
            AddColumn("OrderItem", "DeliveryDate", c => c.DateTime(precision: 0));
            AddColumn("OrderItem", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("OrderItem", "IsCompleted");
            DropColumn("OrderItem", "DeliveryDate");
            DropColumn("OrderItem", "ShippingDate");
            DropColumn("OrderItem", "Tax");
            DropColumn("OrderItem", "LineDiscount");
            DropColumn("OrderItem", "LineAmount");
            DropColumn("OrderItem", "SellPrice");
            DropColumn("OrderItem", "BasePrice");
            DropColumn("Order", "PayableAmount");
            DropColumn("Order", "Tax");
            DropColumn("Order", "Shipping");
            DropColumn("Order", "TotalDiscount");
            DropColumn("Order", "NetAmount");
        }
    }
}
