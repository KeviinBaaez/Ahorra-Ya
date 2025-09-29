namespace AhorraYa.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        //public Category? Category { get; set; }

        public int BrandId { get; set; }
        //public Brand? Brand { get; set; }

        public int UnitId { get; set; }
        //public Brand? Brand { get; set; }

    }
}
