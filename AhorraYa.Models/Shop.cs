using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class Shop : IEntidad
    {
        public Shop()
        {
            PriceOfShops = new HashSet<PriceOfShop>();
        }
        public Shop(string shopName, int locationId)
        {
            SetShopName(shopName);
            SetLocationId(locationId);
        }
        #region Properties
        public int Id { get; set; }
        [StringLength(50)]
        public string ShopName { get; private set; } = null!;

        [ForeignKey(nameof(Location))]  
        public int LocationId { get; private set; }
        #endregion

        #region Virtual
        public virtual Location? Location { get; set; }

        public virtual ICollection<PriceOfShop> PriceOfShops { get; set; }
        #endregion

        #region Getters and Setters
        public void SetShopName(string shopName)
        {
            if (string.IsNullOrEmpty(shopName))
            {
                throw new ArgumentNullException("The shop name cannot be empty");
            }
            ShopName = shopName;
        }

        public void SetLocationId(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentNullException("Enter a valid number (Id)");
            }
            LocationId = locationId;
        }
        #endregion
    }
}
