using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class MeasurementUnit
    {
        public MeasurementUnit()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int UnitId { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public string? Abbreviation { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
