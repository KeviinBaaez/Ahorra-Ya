namespace AhorraYa.Application.Dtos.MeasurementUnit
{
    public class MeasurementUnitResponseDto
    {
        public int Id { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public string? Abbreviation { get; set; }
    }
}
