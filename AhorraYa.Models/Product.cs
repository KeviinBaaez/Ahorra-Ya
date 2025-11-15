using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Net;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AhorraYa.Entities
{
    public class Product : IEntidad
    {
        public Product()
        {
            PriceOfShops = new HashSet<PriceOfShop>();
        }
        public Product(string producName, int categoryId, int brandId, int unitId)
        {
            SetProductName(producName);
            SetCategoryId(categoryId);
            SetBrandId(brandId);
            SetUnitId(unitId);
        }
        #region Properties
        public int Id { get; set; }
        [StringLength(50)]
        public string? ProductName { get; private set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; }
        [JsonIgnore]

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; private set; }
        [JsonIgnore]

        [ForeignKey(nameof(MeasurementUnit))]   
        public int UnitId { get; private set; }
        #endregion

        #region Virtual
        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual MeasurementUnit? MeasurementUnit { get; set; }

        [JsonIgnore]
        public virtual ICollection<PriceOfShop> PriceOfShops { get; set; }
        #endregion

        #region Getters and Setters
        public void SetProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException("The product name cannot be empty");
            }
            ProductName = productName;
        }

        public void SetCategoryId(int categoryId)
        {
            if(categoryId <= 0)
            {
                throw new ArgumentNullException("Enter a valid number (Id)");
            }
            CategoryId = categoryId;
        }

        public void SetBrandId(int brandId)
        {
            if (brandId <= 0)
            {
                throw new ArgumentNullException("Enter a valid number (Id)");
            }
            BrandId = brandId;
        }

        public void SetUnitId(int unitId)
        {
            if (unitId <= 0)
            {
                throw new ArgumentNullException("Enter a valid number (Id)");
            }
            UnitId = unitId;
        }
        #endregion

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Product product)) return false;

            if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(product.ProductName))
                return false;

            // Comparación insensible a mayúsculas/minúsculas
            bool sameProducName = string.Equals(ProductName.Trim(), product.ProductName.Trim(), StringComparison.OrdinalIgnoreCase);
            bool sameBrandId = this.BrandId == product.BrandId;
            bool sameMeaserumentId = this.UnitId == product.UnitId;

            return sameProducName && sameBrandId && sameMeaserumentId;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(
                ProductName?.Trim().ToLowerInvariant(),
                BrandId, UnitId);
        }
    }
}
