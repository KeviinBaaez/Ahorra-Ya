using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class Shop : IEntidad
    {
        public Shop()
        {
            PriceOfShops = new HashSet<PriceOfShop>();
        }
        public int Id { get; set; }
        [StringLength(50)]
        public string ShopName { get; set; } = null!;

        [ForeignKey(nameof(Location))]  
        public int LocationId { get; set; }
        public virtual Location? Location { get; set; }

        public virtual ICollection<PriceOfShop> PriceOfShops { get; set; }
    }
}
