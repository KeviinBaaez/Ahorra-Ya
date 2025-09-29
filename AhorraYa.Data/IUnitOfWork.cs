using AhorraYa.Data.Interfaces;

namespace AhorraYa.Data
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
