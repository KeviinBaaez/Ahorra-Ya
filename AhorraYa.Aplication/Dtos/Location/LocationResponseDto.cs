namespace AhorraYa.Application.Dtos.Location
{
    public class LocationResponseDto
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public int Number { get; set; }
        public int? Floor { get; set; }
    }
}
