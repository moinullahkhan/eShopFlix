using AuthService.Application.Interfaces;
using AuthService.Application.Mappings;
using AuthService.Application.Services;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Repositrories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AuthServiceContext>(options =>
                options.UseSqlServer(connectionString));

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //services
            services.AddScoped<IUserAppService, UserAppService>();

            //mapster
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(AuthRegister).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
