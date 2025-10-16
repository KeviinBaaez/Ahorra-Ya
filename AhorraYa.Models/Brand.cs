using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class Brand : IEntidad
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [StringLength(30)]
        public string BrandName { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }
    }
}
