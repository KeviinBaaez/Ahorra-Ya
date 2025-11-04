using System.ComponentModel.DataAnnotations;

namespace AhorraYa.Application.Dtos.Login
{
    public class LoginUserRequestDto
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
