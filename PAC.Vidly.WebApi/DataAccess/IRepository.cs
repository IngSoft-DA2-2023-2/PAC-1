using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        TEntity? GetOrDefault(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        void Update(TEntity entity);


    }
}
