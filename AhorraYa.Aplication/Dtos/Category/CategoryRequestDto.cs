using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Category
{
    public class CategoryRequestDto
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string CategoryName { get; set; } = null!;
    }
}
