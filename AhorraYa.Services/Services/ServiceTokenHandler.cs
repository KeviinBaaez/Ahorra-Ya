using AhorraYa.Abstractions;
using AhorraYa.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AhorraYa.Services.Services
{
    public class ServiceTokenHandler : IServiceTokenHandler
    {
        private readonly ServiceJwtConfig _jwtConfig;

        public ServiceTokenHandler(IOptionsMonitor<ServiceJwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public string GenerateJwtTokens(ITokenParameters parameters)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = new List<Claim>
            {
                new Claim("Id", parameters.Id),
                new Claim(JwtRegisteredClaimNames.Sub, parameters.Id),
                new Claim(JwtRegisteredClaimNames.Name, parameters.UserName),
                new Claim(JwtRegisteredClaimNames.Email, parameters.Email)
            };
            if (parameters.Roles != null && parameters.Roles.Any())
            {
                foreach (var role in parameters.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
