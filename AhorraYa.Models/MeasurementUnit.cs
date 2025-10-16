using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class MeasurementUnit : IEntidad
    {
        public MeasurementUnit()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [StringLength(40)]
        public string UnitOfMeasure { get; set; } = null!;
        [StringLength(5)]
        public string? Abbreviation { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
