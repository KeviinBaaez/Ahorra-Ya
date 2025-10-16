using AhorraYa.Entities;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Brand
{
    public class BrandRequestDto
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string BrandName { get; set; } = null!;
    }
}
