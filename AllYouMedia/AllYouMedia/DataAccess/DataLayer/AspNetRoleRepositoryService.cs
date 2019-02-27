namespace AllYouMedia.DataAccess.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using System.Linq.Expressions;
    using System.Data;
    using System.Collections.Generic;
    using AllYouMedia.Models;

    public class AspNetRoleRepositoryService : IRepository<AspNetRole>
    {
        private IDbContext context;

        public AspNetRoleRepositoryService(IDbContext context)
        {
            this.context = context;
        }

        private IDbSet<AspNetRole> Entities
        {
            get { return this.context.Set<AspNetRole>(); }
        }

        public IQueryable<AspNetRole> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        public AspNetRole GetById(long id)
        {
            return this.Entities.Find(id);
        }

        public AspNetRole Insert(AspNetRole entity)
        {
            this.Entities.Add(entity);
            this.context.SaveChanges();
            return entity;
        }

        public void Update(AspNetRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var check = this.context.SaveChanges();
        }

        public void Delete(AspNetRole entity)
        {
            this.Entities.Remove(entity);
            this.context.SaveChanges();
        }
        public IQueryable<AspNetRole> GetByQuery(Expression<Func<AspNetRole, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }

        public IQueryable<AspNetRole> GetByAction(Func<IDbSet<AspNetRole>, IQueryable<AspNetRole>> action)
        {
            return action(this.Entities);
        }

        public int GetCount(Expression<Func<AspNetRole, bool>> predicate)
        {
            return this.Entities.Count(predicate);
        }
        public List<DataTable> ExecSql(string CommandText, bool UseTransaction = false)
        {
            //NpgsqlConnection oConn = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //oConn.Open();
            //NpgsqlTransaction tran = null;
            //if (UseTransaction) tran = oConn.BeginTransaction();
            //NpgsqlCommand oCmd = new NpgsqlCommand(CommandText, oConn);
            //NpgsqlDataAdapter oAdapter = new NpgsqlDataAdapter();
            //oAdapter.SelectCommand = oCmd;
            //DataSet oDataset = new DataSet();
            //oAdapter.Fill(oDataset);
            //if (UseTransaction)
            //    tran.Commit();
            //oConn.Close(); oConn.Dispose(); oCmd.Dispose();
            var lDTs = new List<DataTable>();
            //for (int iCount = 0; iCount <= oDataset.Tables.Count - 1; iCount++)
            //{
            //    lDTs.Add(oDataset.Tables[iCount]);
            //}
            //oAdapter.Dispose();
            //oDataset.Dispose();
            return lDTs;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                    this.context = null;
                }
            }
        }
    }
}