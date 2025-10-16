using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.MeasurementUnit
{
    public class MeasurementUnitRequestDto
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string UnitOfMeasure { get; set; } = null!;
        [StringLength(5)]
        public string? Abbreviation { get; set; }
    }
}
