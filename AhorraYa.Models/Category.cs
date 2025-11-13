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
        public Category(string name)
        {
            SetCategoryName(name);
        }
        #region Properties
        public int Id { get; set; }

        [StringLength(30)]
        public string CategoryName { get; private set; } = null!;
        #endregion
        #region Virtual
        public virtual ICollection<Product>? Products { get; set; }
        #endregion
        #region Getters and Setters
        public void SetCategoryName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("The name cannot be empty");
            }
            CategoryName = name;
        }
        #endregion
    }
}
