using Library.DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace Library.DAL.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected ApplicationContext _db;
        private DbSet<TEntity> _dbSet;

        public GenericRepository() : this("DefualtConnection") { }

        public GenericRepository(string connection)
        {
            _db = new ApplicationContext(connection);
            _dbSet = _db.Set<TEntity>();
        }

        public virtual TEntity Create(TEntity item)
        {
            _dbSet.Add(item);
            _db.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _db.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            List<TEntity> result = _dbSet.ToList();
            return result;
        }

        public virtual void Update(TEntity item)
        {
            _db.Set<TEntity>().AddOrUpdate(item);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }


        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
}
