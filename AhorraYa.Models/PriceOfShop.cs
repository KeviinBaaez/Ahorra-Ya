using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhorraYa.Entities
{
    public class PriceOfShop : IEntidad
    {
        public PriceOfShop(decimal price)
        {
            SetPrice(price);
        }
        #region Porperties
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(Shop))]
        public int ShopId { get; set; }
        public decimal Price { get; private set; }
        public DateTime RegistrationDate { get; set; }
        #endregion

        #region Virtual
        public virtual Product? Product { get; set; }
        public virtual Shop? Shop { get; set; }
        #endregion

        #region Getters and Setters
        public void SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentNullException("Enter a valid value");
            }
            Price = price;
        }

        #endregion
    }
}
