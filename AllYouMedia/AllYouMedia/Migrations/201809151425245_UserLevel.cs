namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserLevel : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "UserLevel",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            Name = c.String(maxLength: 200, storeType: "nvarchar"),
            //            IsActive = c.Boolean(nullable: false),
            //            CBOExpression = c.String(maxLength: 500, storeType: "nvarchar"),
            //            CreatedOn = c.DateTime(nullable: false, precision: 0),
            //            ModifiedOn = c.DateTime(precision: 0),
            //        })
            //    .PrimaryKey(t => t.ID);
            //Sql("INSERT INTO UserLevel(ID, Name, IsActive, CreatedOn) VALUES(-1, '', 1, NOW());");
            //AddColumn("AspNetUsers", "UserLevelID", c => c.Long(nullable: false));
            //CreateIndex("AspNetUsers", "UserLevelID");
            Sql("UPDATE AspNetUsers SET UserLevelID=-1;");
            AddForeignKey("AspNetUsers", "UserLevelID", "UserLevel", "ID");
        }

        public override void Down()
        {
            DropForeignKey("AspNetUsers", "UserLevelID", "UserLevel");
            DropIndex("AspNetUsers", new[] { "UserLevelID" });
            DropColumn("AspNetUsers", "UserLevelID");
            DropTable("UserLevel");
        }
    }
}
