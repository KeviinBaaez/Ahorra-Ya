using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Entities.MicrosoftIdentity
{
    public class User : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(20)]
        [PersonalData]
        public string Name { get; set; } = null!;
    }
}
