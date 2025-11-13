using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class Location : IEntidad
    {
        public Location(string address, int number)
        {
            SetAddress(address);
            SetNumber(number);
        }
        #region Properties
        public int Id { get; set; }

        [StringLength(60)]
        public string Address { get; private set; } = null!;
        public int Number { get; private set; }
        public int? Floor { get; set; }
        #endregion

        #region Getters and Setters
        public void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException("The address cannot be empty");
            }
            Address = address;
        }

        public void SetNumber(int number)
        {
            if(number < 0)
            {
                throw new ArgumentNullException("check that the height is a valid number");
            }
            Number = number;
        }
        #endregion
    }
}
