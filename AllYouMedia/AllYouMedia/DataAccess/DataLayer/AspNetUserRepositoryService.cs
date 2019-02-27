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

    public class AspNetUserRepositoryService : IRepository<AspNetUser>
    {
        private IDbContext context;

        public AspNetUserRepositoryService(IDbContext context)
        {
            this.context = context;
        }

        private IDbSet<AspNetUser> Entities
        {
            get { return this.context.Set<AspNetUser>(); }
        }

        public IQueryable<AspNetUser> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        public AspNetUser GetById(long id)
        {
            return this.Entities.Find(id);
        }

        public AspNetUser Insert(AspNetUser entity)
        {
            entity.IsActive = true;
            entity.CreatedOn = DateTime.Now;
            this.Entities.Add(entity);
            this.context.SaveChanges();
            return entity;
        }

        public void Update(AspNetUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.ModifiedOn = DateTime.Now;
            var check = this.context.SaveChanges();
        }

        public void Delete(AspNetUser entity)
        {
            this.Entities.Remove(entity);
            this.context.SaveChanges();
        }
        public IQueryable<AspNetUser> GetByQuery(Expression<Func<AspNetUser, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }

        public IQueryable<AspNetUser> GetByAction(Func<IDbSet<AspNetUser>, IQueryable<AspNetUser>> action)
        {
            return action(this.Entities);
        }

        public int GetCount(Expression<Func<AspNetUser, bool>> predicate)
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