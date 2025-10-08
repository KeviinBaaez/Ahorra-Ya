using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AhorraYa.Services.Interfaces;
using AhorraYa.Services.Services;
using AhorraYa.DataAccess;

namespace AhorraYa.Ioc
{
    public static class DI
    {
        public static IServiceProvider ConfigureDI(IServiceCollection services, IConfiguration configuration)
        {

            //Connection bd
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MyConnection"))); //ahora estamos usando la cadena que esta en el json. att Joaquin

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services.BuildServiceProvider();
        }

    }
}
