using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Brand
{
    public class BrandResponseDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
    }
}
