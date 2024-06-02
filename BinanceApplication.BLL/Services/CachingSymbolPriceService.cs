using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.BLL.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BinanceApplication.BLL.Services
{
    public sealed class CachingSymbolPriceService : ISymbolPriceService
    {
        private static readonly TimeSpan CacheMoving = TimeSpan.FromHours(24);
        private readonly ISymbolPriceService _symbolPriceService;
        private readonly IMemoryCache _memoryCache;

        public Task<AveragePriceDataResponse> GetAveragePrice(string symbol)
        {
            return _symbolPriceService.GetAveragePrice(symbol);
        }

        public Task<List<AverageMovingPrice>> SimpleMovingAverage(SimpleMovingAverageRequest request)
        {
           
            if (request.StartTime.HasValue)
            {
                var requestKey = CreatePartitionKey(request.Symbol, request.StartTime);

                return _memoryCache.GetOrCreateAsync(
                   requestKey,
                   cacheEntry =>
                   {
                       cacheEntry.SetAbsoluteExpiration(CacheMoving);


                       return _symbolPriceService.SimpleMovingAverage(request);
                   });
            }
            else
            {
                return _symbolPriceService.SimpleMovingAverage(request);
            } 
        }


        private string CreatePartitionKey(string symbol, DateTime? startDate)
            => $"{symbol}{startDate.Value.Date}";
    }
}
