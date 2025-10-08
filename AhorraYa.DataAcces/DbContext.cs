using AhorraYa.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AhorraYa.DataAccess
{
    public class DbContext<T> : IDbOperation<T> where T : class
    {
        DbSet<T> _Items;
        DbDataAccess _ctx;
        public DbContext(DbDataAccess ctx)
        {
            _ctx = ctx;
            _Items = ctx.Set<T>();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T Save(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
