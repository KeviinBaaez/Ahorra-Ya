using AhorraYa.Services.Interfaces;

namespace AhorraYa.Services.Services
{
    public class ServiceJwtConfig : IServiceJwtConfig
    {
        public string Secret { get; set; }
    }
}
