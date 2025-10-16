using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Shop
{
    public class ShopRequestDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ShopName { get; set; } = null!;
        public int LocationId { get; set; }
    }
}
