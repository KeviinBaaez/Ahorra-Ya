namespace AhorraYa.Entities
{
    public class Location
    {
        public Location()
        {
            Shops = new HashSet<Shop>();
        }
        public int LocationId { get; set; }
        public string Address { get; set; } = null!;
        public int Number { get; set; }
        public int? Floor { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
