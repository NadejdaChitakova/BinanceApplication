using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.BLL.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BinanceApplication.BLL.Services
{
    public sealed class CachingSymbolPriceService(
        ISymbolPriceService symbolPriceService,
        IMemoryCache memoryCache)
        : ISymbolPriceService
    {
        private static readonly TimeSpan CacheMoving = TimeSpan.FromHours(24);

        public Task<AveragePriceDataResponse> GetAveragePrice(string symbol)
        {
            return symbolPriceService.GetAveragePrice(symbol);
        }

        public Task<List<AverageMovingPrice>?> SimpleMovingAverage(SimpleMovingAverageRequest request)
        {
           
            if (request.StartTime.HasValue)
            {
                var requestKey = CreatePartitionKey(request.Symbol, request.StartTime);
                
                return memoryCache.GetOrCreateAsync(
                   requestKey,
                   cacheEntry =>
                   {
                       cacheEntry.SetAbsoluteExpiration(CacheMoving);


                       return symbolPriceService.SimpleMovingAverage(request);
                   });
            }
            else
            {
                return symbolPriceService.SimpleMovingAverage(request);
            } 
        }

        private string CreatePartitionKey(string symbol, DateTime? startDate)
            => $"{symbol}{startDate.Value.Date}";
    }
}
