using PAC.Vidly.WebApi.Services.Movies.Entities;
using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.DataAccess
{
    public sealed class MovieRepository : Repository<Movie>
    {
        public MovieRepository(PacVidlyDbContext context) : base(context)
        {
        }

        public override Movie? GetOrDefault(Expression<Func<Movie, bool>> predicate)
        {
            return base.GetOrDefault(predicate);
            /*var query = _entities.Where(predicate);*/ // falta insetar logica para obtener las peliculas favoritas y usuarios
        }
    }
}
