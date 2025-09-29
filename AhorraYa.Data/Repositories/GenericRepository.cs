using AhorraYa.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AhorraYa.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        protected AppDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>>? filter = null, string? propertiesName = null, bool tracked = false)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(propertiesName != null)
            {
                foreach(var propertyInclude in propertiesName
                    .Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertyInclude);
                }
            }
            if (tracked)
            {
                return query.FirstOrDefault();
            }
            return query.AsNoTracking().FirstOrDefault();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            string? propertiesName = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(propertiesName != null)
            {
                foreach(var propertyInclude in propertiesName
                    .Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertyInclude);
                }
            }
            if(orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
