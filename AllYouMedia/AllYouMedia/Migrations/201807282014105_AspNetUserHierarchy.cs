namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AspNetUserHierarchy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("AspNetUserAddress", "AspNetUserID", "AspNetUsers");
            DropForeignKey("Order", "AspNetUserAddressID", "AspNetUserAddress");
            DropForeignKey("AspNetUserClaims", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserLogins", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "UserId", "AspNetUsers");
            DropForeignKey("Order", "AspNetUserID", "AspNetUsers");
            DropForeignKey("OrderItem", "OrderID", "Order");
            DropForeignKey("OrderItem", "ItemID", "Item");
            DropForeignKey("CartItem", "ItemID", "Item");
            DropForeignKey("ItemAttribute", "ItemID", "Item");
            DropForeignKey("Item", "ProductionID", "Production");
            DropForeignKey("Item", "SubCategoryID", "SubCategory");
            DropForeignKey("CartItem", "CartID", "Cart");
            DropForeignKey("Cart", "AspNetUserID", "AspNetUsers");
            DropForeignKey("ItemAttribute", "AttributeID", "Attribute");
            DropForeignKey("Production", "AspNetUserID", "AspNetUsers");
            DropForeignKey("SubCategory", "CategoryID", "Category");
            DropForeignKey("Category", "CategoryTypeID", "CategoryType");
            DropForeignKey("Log", "AspNetUserID", "AspNetUsers");
            DropForeignKey("Message", "FromAspNetUserID", "AspNetUsers");
            DropForeignKey("Message", "ToAspNetUserID", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles");
            CreateTable(
                "AspNetUserHierarchy",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AspNetUserID = c.Long(nullable: false),
                        ParentAspNetUserID = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.AspNetUserID)
                .Index(t => t.ParentAspNetUserID);
            AddForeignKey("AspNetUserHierarchy", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("AspNetUserHierarchy", "ParentAspNetUserID", "AspNetUsers", "Id");

            AddColumn("AspNetUsers", "RefferCode", c => c.String(maxLength: 200, storeType: "nvarchar"));
            AlterColumn("CartItem", "Qty", c => c.Int(nullable: false));
            AddForeignKey("AspNetUserAddress", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("Order", "AspNetUserAddressID", "AspNetUserAddress", "ID");
            AddForeignKey("AspNetUserClaims", "UserId", "AspNetUsers", "Id");
            AddForeignKey("AspNetUserLogins", "UserId", "AspNetUsers", "Id");
            AddForeignKey("AspNetUserRoles", "UserId", "AspNetUsers", "Id");
            AddForeignKey("Order", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("OrderItem", "OrderID", "Order", "ID");
            AddForeignKey("OrderItem", "ItemID", "Item", "ID");
            AddForeignKey("CartItem", "ItemID", "Item", "ID");
            AddForeignKey("ItemAttribute", "ItemID", "Item", "ID");
            AddForeignKey("Item", "ProductionID", "Production", "ID");
            AddForeignKey("Item", "SubCategoryID", "SubCategory", "ID");
            AddForeignKey("CartItem", "CartID", "Cart", "ID");
            AddForeignKey("Cart", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("ItemAttribute", "AttributeID", "Attribute", "ID");
            AddForeignKey("Production", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("SubCategory", "CategoryID", "Category", "ID");
            AddForeignKey("Category", "CategoryTypeID", "CategoryType", "ID");
            AddForeignKey("Log", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("Message", "FromAspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("Message", "ToAspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles", "Id");
            DropColumn("AspNetUsers", "RefferedByUserName");
        }

        public override void Down()
        {
            AddColumn("AspNetUsers", "RefferedByUserName", c => c.String(maxLength: 200, storeType: "nvarchar"));
            DropForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles");
            DropForeignKey("Message", "ToAspNetUserID", "AspNetUsers");
            DropForeignKey("Message", "FromAspNetUserID", "AspNetUsers");
            DropForeignKey("Log", "AspNetUserID", "AspNetUsers");
            DropForeignKey("Category", "CategoryTypeID", "CategoryType");
            DropForeignKey("SubCategory", "CategoryID", "Category");
            DropForeignKey("Production", "AspNetUserID", "AspNetUsers");
            DropForeignKey("ItemAttribute", "AttributeID", "Attribute");
            DropForeignKey("Cart", "AspNetUserID", "AspNetUsers");
            DropForeignKey("CartItem", "CartID", "Cart");
            DropForeignKey("Item", "SubCategoryID", "SubCategory");
            DropForeignKey("Item", "ProductionID", "Production");
            DropForeignKey("ItemAttribute", "ItemID", "Item");
            DropForeignKey("CartItem", "ItemID", "Item");
            DropForeignKey("OrderItem", "ItemID", "Item");
            DropForeignKey("OrderItem", "OrderID", "Order");
            DropForeignKey("Order", "AspNetUserID", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserLogins", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserClaims", "UserId", "AspNetUsers");
            DropForeignKey("Order", "AspNetUserAddressID", "AspNetUserAddress");
            DropForeignKey("AspNetUserAddress", "AspNetUserID", "AspNetUsers");
            DropForeignKey("AspNetUserHierarchy", "ParentAspNetUserID", "AspNetUsers");
            DropForeignKey("AspNetUserHierarchy", "AspNetUserID", "AspNetUsers");
            DropIndex("AspNetUserHierarchy", new[] { "ParentAspNetUserID" });
            DropIndex("AspNetUserHierarchy", new[] { "AspNetUserID" });
            AlterColumn("CartItem", "Qty", c => c.Int());
            DropColumn("AspNetUsers", "RefferCode");
            DropTable("AspNetUserHierarchy");
            AddForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("Message", "ToAspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("Message", "FromAspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("Log", "AspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("Category", "CategoryTypeID", "CategoryType", "ID", cascadeDelete: true);
            AddForeignKey("SubCategory", "CategoryID", "Category", "ID", cascadeDelete: true);
            AddForeignKey("Production", "AspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("ItemAttribute", "AttributeID", "Attribute", "ID", cascadeDelete: true);
            AddForeignKey("Cart", "AspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("CartItem", "CartID", "Cart", "ID", cascadeDelete: true);
            AddForeignKey("Item", "SubCategoryID", "SubCategory", "ID", cascadeDelete: true);
            AddForeignKey("Item", "ProductionID", "Production", "ID", cascadeDelete: true);
            AddForeignKey("ItemAttribute", "ItemID", "Item", "ID", cascadeDelete: true);
            AddForeignKey("CartItem", "ItemID", "Item", "ID", cascadeDelete: true);
            AddForeignKey("OrderItem", "ItemID", "Item", "ID", cascadeDelete: true);
            AddForeignKey("OrderItem", "OrderID", "Order", "ID", cascadeDelete: true);
            AddForeignKey("Order", "AspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("AspNetUserRoles", "UserId", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("AspNetUserLogins", "UserId", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("AspNetUserClaims", "UserId", "AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("Order", "AspNetUserAddressID", "AspNetUserAddress", "ID", cascadeDelete: true);
            AddForeignKey("AspNetUserAddress", "AspNetUserID", "AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
