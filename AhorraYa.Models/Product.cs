using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AhorraYa.Entities
{
    public class Product : IEntidad
    {
        public Product()
        {
            PriceOfShops = new HashSet<PriceOfShop>();
        }
        public int Id { get; set; }
        [StringLength(50)]
        public string? ProductName { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        [JsonIgnore]
        public virtual Brand? Brand { get; set; }

        [ForeignKey(nameof(MeasurementUnit))]   
        public int UnitId { get; set; }
        [JsonIgnore]
        public virtual MeasurementUnit? MeasurementUnit { get; set; }

        public virtual ICollection<PriceOfShop> PriceOfShops { get; set; }

    }
}
