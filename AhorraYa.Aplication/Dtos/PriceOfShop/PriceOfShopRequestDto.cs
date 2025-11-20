using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.PriceOfShop
{
    public class PriceOfShopRequestDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        [Range(0.01, 999999.99)]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
    }
}
