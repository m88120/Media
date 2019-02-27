namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AspNetUser_PhotoIMG : DbMigration
    {
        public override void Up()
        {
            AddColumn("AspNetUsers", "SubCategoryID", c => c.Long(nullable: true));
            Sql("UPDATE AspNetUsers SET SubCategoryID=-1");
            AlterColumn("AspNetUsers", "SubCategoryID", c => c.Long(nullable: false));
            AddColumn("AspNetUsers", "PhotoIMG", c => c.String(maxLength: 2000, storeType: "nvarchar"));
            AddColumn("AspNetUsers", "BioDescription", c => c.String(unicode: false));
            CreateIndex("AspNetUsers", "SubCategoryID");
            AddForeignKey("AspNetUsers", "SubCategoryID", "SubCategory", "ID");
        }

        public override void Down()
        {
            DropForeignKey("AspNetUsers", "SubCategoryID", "SubCategory");
            DropIndex("AspNetUsers", new[] { "SubCategoryID" });
            DropColumn("AspNetUsers", "BioDescription");
            DropColumn("AspNetUsers", "PhotoIMG");
            DropColumn("AspNetUsers", "SubCategoryID");
        }
    }
}
