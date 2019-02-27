namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CBOExpressionInAllTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("AspNetUserAddress", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Order", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("OrderItem", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Item", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("CartItem", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Cart", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("ItemAttribute", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Attribute", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Production", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("SubCategory", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Category", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("CategoryType", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Log", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
            AddColumn("Message", "CBOExpression", c => c.String(maxLength: 500, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Message", "CBOExpression");
            DropColumn("Log", "CBOExpression");
            DropColumn("CategoryType", "CBOExpression");
            DropColumn("Category", "CBOExpression");
            DropColumn("SubCategory", "CBOExpression");
            DropColumn("Production", "CBOExpression");
            DropColumn("Attribute", "CBOExpression");
            DropColumn("ItemAttribute", "CBOExpression");
            DropColumn("Cart", "CBOExpression");
            DropColumn("CartItem", "CBOExpression");
            DropColumn("Item", "CBOExpression");
            DropColumn("OrderItem", "CBOExpression");
            DropColumn("Order", "CBOExpression");
            DropColumn("AspNetUserAddress", "CBOExpression");
        }
    }
}
