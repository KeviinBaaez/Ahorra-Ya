using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Product
{
    public class ProductRequestDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int UnitId { get; set; }
    }
}
