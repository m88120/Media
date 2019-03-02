namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryExtended : DbMigration
    {
        public override void Up()
        {
            AddColumn("Category", "IsExtended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Category", "IsExtended");
        }
    }
}
