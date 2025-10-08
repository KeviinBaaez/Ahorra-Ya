using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class PriceOfShop
    {
        public int PriceOfShopId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(Shop))]
        public int ShopId { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Shop? Shop { get; set; }
    }
}
