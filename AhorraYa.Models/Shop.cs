using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; } = null!;

        [ForeignKey(nameof(Location))]  
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
