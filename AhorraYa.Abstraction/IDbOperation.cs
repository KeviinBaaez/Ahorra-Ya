namespace AhorraYa.Abstractions
{
    public interface IDbOperation<T>
    {
        T Save (T entity);
        IList<T> GetAll ();
        T GetById (int id);
        void RemoveById (int id);
    }
}
