using BinanceApplication.BLL.Repositories;
using BinanceApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceApplication.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<IdentityCoreDbContext>(options => { options.UseSqlServer(connectionString); });

            services.AddScoped<ISymbolPriceRepository, SymbolPriceRepository>();

            return services;
        }
    }
}
