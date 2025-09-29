using AhorraYa.Models.Entities;

namespace AhorraYa.Services.Interfaces
{
    public interface IProductService
    {
        Product? GetProductById(int id);
    }
}
