namespace AllYouMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserCategory_Role : DbMigration
    {
        public override void Up()
        {
            Sql("START TRANSACTION;");
            //DropForeignKey("UserCategoryMap", "AspNetUserID", "AspNetUsers");
            //DropForeignKey("UserCategoryMap", "AspNetUserRoleID", "AspNetUserRoles");
            //DropIndex("UserCategoryMap", new[] { "AspNetUserID" });
            //DropPrimaryKey("AspNetUserRoles");
            //AddColumn("UserCategoryMap", "AspNetUserRoleID", c => c.Long(nullable: false));
            //AddColumn("AspNetUserRoles", "ID", c => c.Long(nullable: true, identity: true));
            //var sb = new System.Text.StringBuilder();
            //sb.AppendLine("SET @DemoVal = 0;");
            //sb.AppendLine("UPDATE AspNetUserRoles");
            //sb.AppendLine("SET ID=@DemoVal:=@DemoVal+1");
            //sb.AppendLine("WHERE ID=NULL");
            //Sql(sb.ToString());
            //AlterColumn("AspNetUserRoles", "ID", c => c.Long(nullable: false, identity: true));
            //AddPrimaryKey("AspNetUserRoles", "ID");
            //CreateIndex("UserCategoryMap", "AspNetUserRoleID");
            //AddForeignKey("UserCategoryMap", "AspNetUserRoleID", "AspNetUserRoles", "ID");
            DropColumn("UserCategoryMap", "AspNetUserID");
            Sql("COMMIT;");
        }

        public override void Down()
        {
            AddColumn("UserCategoryMap", "AspNetUserID", c => c.Long(nullable: false));
            DropForeignKey("UserCategoryMap", "AspNetUserRoleID", "AspNetUserRoles");
            DropIndex("UserCategoryMap", new[] { "AspNetUserRoleID" });
            DropPrimaryKey("AspNetUserRoles");
            DropColumn("AspNetUserRoles", "ID");
            DropColumn("UserCategoryMap", "AspNetUserRoleID");
            AddPrimaryKey("AspNetUserRoles", new[] { "UserId", "RoleId" });
            CreateIndex("UserCategoryMap", "AspNetUserID");
            AddForeignKey("UserCategoryMap", "AspNetUserRoleID", "AspNetUserRoles", "ID");
            AddForeignKey("UserCategoryMap", "AspNetUserID", "AspNetUsers", "Id");
        }
    }
}
