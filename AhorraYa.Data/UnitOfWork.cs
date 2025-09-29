using AhorraYa.Data.Interfaces;
using AhorraYa.Data.Repositories;

namespace AhorraYa.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IProductRepository _product;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IProductRepository Products
        {
            get
            {
                _product ??= new ProductRepository(_dbContext); return _product;
            }
        }
    }
}
