
using AhorraYa.Abstractions;
using AhorraYa.Application;
using AhorraYa.Application.Interfaces;
using AhorraYa.DataAccess;
using AhorraYa.Repository.Interfaces;
using AhorraYa.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace AhorraYa.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            builder.Services.AddDbContext<DbDataAccess>(options =>
            {
                options.UseSqlServer(builder.Configuration
                    .GetConnectionString("MyConnection"),
                    o => o.MigrationsAssembly("AhorraYa.WebApi"));
                options.UseLazyLoadingProxies();
            });
            //DI
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
            builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
