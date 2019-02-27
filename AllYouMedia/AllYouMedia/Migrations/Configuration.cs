using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Data.Entity.Core.EntityClient;

namespace AllYouMedia.Migrations
{
   

    internal sealed class Configuration : DbMigrationsConfiguration<AllYouMedia.DataAccess.DataLayer.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new NoHash_MySqlMigrationSqlGenerator());
        }

        protected override void Seed(AllYouMedia.DataAccess.DataLayer.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
    public class NoHash_MySqlMigrationSqlGenerator : MySql.Data.Entity.MySqlMigrationSqlGenerator
    {
        private string TrimSchemaPrefix(string table)
        {
            if (table.StartsWith("dbo."))
                return table.Replace("dbo.", "");

            return table;
        }
        protected override System.Data.Entity.Migrations.Sql.MigrationStatement Generate(System.Data.Entity.Migrations.Model.CreateIndexOperation op)
        {
            StringBuilder sb = new StringBuilder();

            sb = sb.Append("CREATE ");

            if (op.IsUnique)
            {
                sb.Append("UNIQUE ");
            }

            //index_col_name specification can end with ASC or DESC.
            // sort order are permitted for future extensions for specifying ascending or descending index value storage
            //Currently, they are parsed but ignored; index values are always stored in ascending order.

            object sort;
            op.AnonymousArguments.TryGetValue("Sort", out sort);
            var sortOrder = sort != null && sort.ToString() == "Ascending" ?
                            "ASC" : "DESC";

            sb.AppendFormat("index  `{0}` on `{1}` (", op.Name, TrimSchemaPrefix(op.Table));
            sb.Append(string.Join(",", op.Columns.Select(c => "`" + c + "` " + sortOrder)) + ") ");

            object indexTypeDefinition;
            op.AnonymousArguments.TryGetValue("Type", out indexTypeDefinition);
            var indexType = indexTypeDefinition != null && string.Compare(indexTypeDefinition.ToString(), "BTree", StringComparison.InvariantCultureIgnoreCase) > 0 ?
                            "BTREE" : "HASH";

            sb.Append("using " + indexType);  ////////Causing error 

            return new System.Data.Entity.Migrations.Sql.MigrationStatement() { Sql = sb.ToString() };
        }
    }
}
