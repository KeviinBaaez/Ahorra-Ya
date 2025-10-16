using AhorraYa.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities
{
    public class Location : IEntidad
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string Address { get; set; } = null!;
        public int Number { get; set; }
        public int? Floor { get; set; }
    }
}
