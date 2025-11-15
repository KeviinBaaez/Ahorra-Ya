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

        public bool Exist(T entity)
        {
            return _dbContext.Exist(entity);
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
    }
}
