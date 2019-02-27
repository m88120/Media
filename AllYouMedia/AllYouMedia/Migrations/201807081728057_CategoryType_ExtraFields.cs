namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryType_ExtraFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("CategoryType", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("CategoryType", "CreatedOn", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("CategoryType", "ModifiedOn", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("CategoryType", "ModifiedOn");
            DropColumn("CategoryType", "CreatedOn");
            DropColumn("CategoryType", "IsActive");
        }
    }
}
