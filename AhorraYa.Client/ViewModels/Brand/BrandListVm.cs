using System.ComponentModel;

namespace AhorraYa.WebClient.ViewModels.Brand
{
    public class BrandListVm
    {
        public int Id { get; set; }
        [DisplayName("Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
