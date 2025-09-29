using AhorraYa.Data;
using AhorraYa.Models.Entities;
using AhorraYa.Services.Interfaces;

namespace AhorraYa.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Product? GetProductById(int id)
        {
            var product = _unitOfWork.Products
                .Get(filter: p => p.ProductId == id, tracked: true);
            if (product is null) return null;
            return product;
        }
    }
}
