﻿using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
            _dbContext.SaveChanges();
        }

        public TEntity? GetOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _entities.Where(predicate);

            var entity = query.FirstOrDefault();

            return entity;
        }

        public bool Any(Func<object, bool> value)
        {
            return _entities.Any(value);
        }

    }
}
