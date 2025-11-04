using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Identity.User
{
    public class UserRegisterRequestDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
