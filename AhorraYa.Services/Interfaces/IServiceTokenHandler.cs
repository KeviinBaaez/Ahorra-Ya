using AhorraYa.Abstractions;

namespace AhorraYa.Services.Interfaces
{
    public interface IServiceTokenHandler
    {
        string GenerateJwtTokens(ITokenParameters parameters);
    }
}
