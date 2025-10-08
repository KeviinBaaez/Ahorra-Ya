namespace AhorraYa.Abstractions
{
    public interface IDbContext<T> : IDbOperation<T> where T : class
    {
    }
}
//34.50

//FLOW