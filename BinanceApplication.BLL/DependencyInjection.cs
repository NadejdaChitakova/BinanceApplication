using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceApplication.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IConverterService, ConverterService>();
            services.AddScoped<ISymbolPriceService, SymbolPriceService>();
            //services.AddHostedService<BackgroundService>();

            return services;
        }
    }
}
