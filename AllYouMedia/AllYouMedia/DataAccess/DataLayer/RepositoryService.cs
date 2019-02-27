namespace AllYouMedia.DataAccess.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using System.Linq.Expressions;
    using System.Data;
    using System.Collections.Generic;
    using MySql.Data.MySqlClient;

    public class RepositoryService<TEntity> : IRepository<TEntity> where TEntity : AllYouMedia.DataAccess.EntityLayer.BaseEntity
    {
        private IDbContext context;

        public RepositoryService(IDbContext context)
        {
            this.context = context;
        }

        private IDbSet<TEntity> Entities
        {
            get { return this.context.Set<TEntity>(); }
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        public TEntity GetById(long id)
        {
            return this.Entities.Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            entity.IsActive = true;
            entity.CreatedOn = DateTime.Now;
            this.Entities.Add(entity);
            this.context.SaveChanges();
            return entity;
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.ModifiedOn = DateTime.Now;
            var check = this.context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.Entities.Remove(entity);
            this.context.SaveChanges();
        }
        public IQueryable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }
        public IQueryable<TEntity> GetByAction(Func<IDbSet<TEntity>, IQueryable<TEntity>> action)
        {
            return action(this.Entities);
        }
        public List<DataTable> ExecSql(string CommandText, bool UseTransaction = false)
        {
            MySqlConnection oConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            oConn.Open();
            MySqlTransaction tran = null;
            if (UseTransaction) tran = oConn.BeginTransaction();
            MySqlCommand oCmd = new MySqlCommand(CommandText, oConn);
            MySqlDataAdapter oAdapter = new MySqlDataAdapter();
            oAdapter.SelectCommand = oCmd;
            DataSet oDataset = new DataSet();
            oAdapter.Fill(oDataset);
            if (UseTransaction)
                tran.Commit();
            oConn.Close(); oConn.Dispose(); oCmd.Dispose();
            var lDTs = new List<DataTable>();
            for (int iCount = 0; iCount <= oDataset.Tables.Count - 1; iCount++)
            {
                lDTs.Add(oDataset.Tables[iCount]);
            }
            oAdapter.Dispose();
            oDataset.Dispose();
            return lDTs;
        }

        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.Count(predicate);
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