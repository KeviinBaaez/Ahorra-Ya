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
    }
}
