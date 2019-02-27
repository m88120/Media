namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CategoryType",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("Category", "CategoryTypeID", c => c.Long(nullable: false));
            AlterColumn("Category", "PhotoIMG", c => c.String(maxLength: 2000, storeType: "nvarchar"));
            CreateIndex("Category", "CategoryTypeID");
            AddForeignKey("Category", "CategoryTypeID", "CategoryType", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Category", "CategoryTypeID", "CategoryType");
            DropIndex("Category", new[] { "CategoryTypeID" });
            AlterColumn("Category", "PhotoIMG", c => c.String(maxLength: 200, storeType: "nvarchar"));
            DropColumn("Category", "CategoryTypeID");
            DropTable("CategoryType");
        }
    }
}
