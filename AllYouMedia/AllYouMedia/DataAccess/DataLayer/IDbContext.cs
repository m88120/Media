namespace AllYouMedia.DataAccess.DataLayer
{
    using System.Data.Entity;

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}
