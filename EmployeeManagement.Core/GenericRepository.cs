using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private dbContext _context;
        private DbSet<TEntity> _dbset;

        public GenericRepository(dbContext dbContext)
        {
            _context = dbContext;
            _dbset = _context.Set<TEntity>();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] navigationProperty)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> expression in navigationProperty)
            {
                dbQuery = dbQuery.AsNoTracking().Include(expression);
            }

            if (where != null)
            {
                list = dbQuery.AsNoTracking().Where(where).ToList();
            }
            else
            {
                list = dbQuery.AsNoTracking().ToList();
            }

            return list;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;

            IQueryable<TEntity> dbQuery = _dbset;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.AsNoTracking().Include(navigationProperty);
            }

            item = dbQuery.FirstOrDefault(where);

            return item;
        }

        public TEntity Add(TEntity item)
        {
            _context.Entry(item).State = EntityState.Added;
            return item;
        }

        public TEntity Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public TEntity Remove(TEntity item)
        {
            _context.Entry(item).State = EntityState.Deleted;
            return item;
        }

        public bool Remove(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = _dbset;
            dbQuery = _dbset.AsQueryable().Where(where);

            foreach (TEntity item in dbQuery)
            {
                _dbset.Remove(item);
            }

            return true;
        }

        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            return  _dbset.Any(where);
        }

    }
}
