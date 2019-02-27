namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_PayerRefCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("Order", "PayerRefCode", c => c.String(maxLength: 200, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Order", "PayerRefCode");
        }
    }
}
