using AhorraYa.Abstractions;

namespace AhorraYa.Repository.Interfaces
{
    public interface IGenericRepository<T> : IDbOperation<T>
    {
        //IQueryable<T> GetAll(
        //    Expression<Func<T, bool>>? filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        //    string? propertiesName = null);

        //T? Get(
        //    Expression<Func<T, bool>>? filter = null,
        //    string? propertiesName = null,
        //    bool tracked = false);

        //void Add(T entity);

        //void Remove(T entity);
    }
}
