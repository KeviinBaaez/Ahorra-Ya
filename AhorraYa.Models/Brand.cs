using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhorraYa.Entities
{
    public class Brand : IEntidad
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }
        public Brand(string name)
        {
            SetBrandName(name);
        }

        #region Properties
        public int Id { get; set; }
        [StringLength(30)]
        public string BrandName { get; private set; } = null!;
        #endregion
        #region Virtual
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
        #endregion
        #region Getters and Setters
        public void SetBrandName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("The name cannot be empty");
            }
            BrandName = name;
        }

        #endregion

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if(obj is null || !(obj is Brand brand)) return false;

            if (string.IsNullOrWhiteSpace(BrandName) || string.IsNullOrWhiteSpace(brand.BrandName))
                return false;

            // Comparación insensible a mayúsculas/minúsculas
            return string.Equals(this.BrandName.Trim(), brand.BrandName.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return BrandName?.Trim().ToLowerInvariant().GetHashCode() ?? 0;
        }
    }
}
