namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerModuleFieldSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("Item", "SellPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Category", "PhotoIMG", c => c.String(maxLength: 2000, storeType: "nvarchar"));
            AlterColumn("AspNetUserAddress", "City", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("AspNetUserAddress", "Province", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("AspNetUserAddress", "Country", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("AspNetUserAddress", "PinCode", c => c.String(maxLength: 20, storeType: "nvarchar"));
            AlterColumn("Order", "PaymentMode", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Order", "PaymentRefCode", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Item", "Name", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Item", "PhotoIMG", c => c.String(maxLength: 2000, storeType: "nvarchar"));
            AlterColumn("Attribute", "Name", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Production", "Name", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("SubCategory", "Name", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Category", "Name", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Log", "ActivityType", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("Message", "Subject", c => c.String(maxLength: 200, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("Message", "Subject", c => c.String(unicode: false));
            AlterColumn("Log", "ActivityType", c => c.String(unicode: false));
            AlterColumn("Category", "Name", c => c.String(unicode: false));
            AlterColumn("SubCategory", "Name", c => c.String(unicode: false));
            AlterColumn("Production", "Name", c => c.String(unicode: false));
            AlterColumn("Attribute", "Name", c => c.String(unicode: false));
            AlterColumn("Item", "PhotoIMG", c => c.String(unicode: false));
            AlterColumn("Item", "Name", c => c.String(unicode: false));
            AlterColumn("Order", "PaymentRefCode", c => c.String(unicode: false));
            AlterColumn("Order", "PaymentMode", c => c.String(unicode: false));
            AlterColumn("AspNetUserAddress", "PinCode", c => c.String(unicode: false));
            AlterColumn("AspNetUserAddress", "Country", c => c.String(unicode: false));
            AlterColumn("AspNetUserAddress", "Province", c => c.String(unicode: false));
            AlterColumn("AspNetUserAddress", "City", c => c.String(unicode: false));
            DropColumn("Category", "PhotoIMG");
            DropColumn("Item", "SellPrice");
        }
    }
}
