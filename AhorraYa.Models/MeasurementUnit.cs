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

        public MeasurementUnit(string abbreviation)
        {
            SetUnitOfMeasure(abbreviation);
        }

        #region Properties
        public int Id { get; set; }

        [StringLength(40)]
        public string UnitOfMeasure { get; private set; } = null!;
        [StringLength(5)]
        public string? Abbreviation { get; set; }
        #endregion

        #region Virtual
        public virtual ICollection<Product>? Products { get; set; }
        #endregion

        #region Getters and Setters
        public void SetUnitOfMeasure(string unitOfMeasure)
        {
            if (string.IsNullOrEmpty(unitOfMeasure))
            {
                throw new ArgumentNullException("The unit of measure cannot be empty");
            }
            UnitOfMeasure = unitOfMeasure;
        }
        #endregion

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is MeasurementUnit measurement)) return false;

            if (string.IsNullOrWhiteSpace(UnitOfMeasure) || string.IsNullOrWhiteSpace(measurement.UnitOfMeasure))
                return false;

            // Comparación insensible a mayúsculas/minúsculas
            return string.Equals(this.UnitOfMeasure.Trim(), measurement.UnitOfMeasure.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return UnitOfMeasure?.Trim().ToLowerInvariant().GetHashCode() ?? 0;
        }
    }
}
