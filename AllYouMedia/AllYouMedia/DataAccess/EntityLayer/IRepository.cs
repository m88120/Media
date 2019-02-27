namespace AllYouMedia.DataAccess.EntityLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(long id);

        TEntity Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> predicate);

        List<DataTable> ExecSql(string CommandText, bool UseTransaction = false);

        int GetCount(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetByAction(Func<System.Data.Entity.IDbSet<TEntity>, IQueryable<TEntity>> action);

    }
}