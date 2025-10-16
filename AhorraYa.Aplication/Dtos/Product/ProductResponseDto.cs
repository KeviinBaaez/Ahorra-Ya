namespace AhorraYa.Application.Dtos.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
