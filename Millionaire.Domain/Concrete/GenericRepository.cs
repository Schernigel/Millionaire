using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace Millionaire.Domain.Concrete
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private EFDbContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(EFDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            var entity = _dbSet.FirstOrDefault(predicate);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}