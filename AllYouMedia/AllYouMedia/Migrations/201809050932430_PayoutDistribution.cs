namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayoutDistribution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PayoutDistribution",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderID = c.Long(nullable: false),
                        AspNetUserID = c.Long(nullable: false),
                        PayoutBaseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayoutPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceivedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAmountReleased = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("AspNetUsers", t => t.AspNetUserID)
                //.ForeignKey("Order", t => t.OrderID)
                .Index(t => t.OrderID)
                .Index(t => t.AspNetUserID);
            AddForeignKey("PayoutDistribution", "AspNetUserID", "AspNetUsers", "Id");
            AddForeignKey("PayoutDistribution", "OrderID", "Order", "ID");
            AddColumn("AspNetUsers", "EarningAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Order", "IsPayoutCreated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("PayoutDistribution", "OrderID", "Order");
            DropForeignKey("PayoutDistribution", "AspNetUserID", "AspNetUsers");
            DropIndex("PayoutDistribution", new[] { "AspNetUserID" });
            DropIndex("PayoutDistribution", new[] { "OrderID" });
            DropColumn("Order", "IsPayoutCreated");
            DropColumn("AspNetUsers", "EarningAmount");
            DropTable("PayoutDistribution");
        }
    }
}
