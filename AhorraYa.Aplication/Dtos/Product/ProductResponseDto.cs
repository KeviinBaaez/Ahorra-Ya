namespace AhorraYa.Application.Dtos.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string UnitName { get; set; } = null!;

    }
}
