using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class Category : IEntidad
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }
    }
}
