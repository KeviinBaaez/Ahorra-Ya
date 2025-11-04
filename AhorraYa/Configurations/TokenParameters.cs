using AhorraYa.Abstractions;

namespace AhorraYa.WebApi.Configurations
{
    public class TokenParameters : ITokenParameters
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
    }
}
