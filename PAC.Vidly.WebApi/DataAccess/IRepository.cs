using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        TEntity? GetOrDefault(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetAll();

        void Update(TEntity entity);


    }
}
