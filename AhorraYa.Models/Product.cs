using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        [ForeignKey(nameof(MeasurementUnit))]   
        public int UnitId { get; set; }
        public virtual MeasurementUnit? MeasurementUnit { get; set; }

    }
}
