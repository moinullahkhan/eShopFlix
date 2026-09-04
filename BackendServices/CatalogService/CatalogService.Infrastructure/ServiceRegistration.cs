using CatalogService.Application.Interfaces;
using CatalogService.Application.Mappings;
using CatalogService.Application.Services;
using CatalogService.Domain.Interfaces;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Persistence.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogServiceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductAppService, ProductAppService>();

            //Mapster For Custom Mappings
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(CatalogRegister).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
