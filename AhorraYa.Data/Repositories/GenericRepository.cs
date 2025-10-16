using AhorraYa.Abstractions;
using AhorraYa.Repository.Interfaces;

namespace AhorraYa.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDbContext<T> _dbContext;

        public GenericRepository(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<T> GetAll()
        {
            return _dbContext.GetAll();
        }

        public T GetById(int id)
        {
            return _dbContext.GetById(id);
        }

        public void RemoveById(int id)
        {
            _dbContext.RemoveById(id);
        }

        public T Save(T entity)
        {
            return _dbContext.Save(entity);
        }



        //public T? GetById(Expression<Func<T, bool>>? filter = null, string? propertiesName = null, bool tracked = false)
        //{

        //}

        //public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, 
        //    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
        //    string? propertiesName = null)
        //{
        //    IQueryable<T> query = _dbSet.AsQueryable();
        //    if(filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    if(propertiesName != null)
        //    {
        //        foreach(var propertyInclude in propertiesName
        //            .Split(",", StringSplitOptions.RemoveEmptyEntries))
        //        {        //    IQueryable<T> query = _dbSet.AsQueryable();
        //    if(filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    if(propertiesName != null)
        //    {
        //        foreach(var propertyInclude in propertiesName
        //            .Split(",", StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(propertyInclude);
        //        }
        //    }
        //    if (tracked)
        //    {
        //        return query.FirstOrDefault();
        //    }
        //    return query.AsNoTracking().FirstOrDefault();
        //            query = query.Include(propertyInclude);
        //        }
        //    }
        //    if(orderBy != null)
        //    {
        //        query = orderBy(query);
        //    }
        //    return query;
        //}

        //public void RemoveById(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}
    }
}
