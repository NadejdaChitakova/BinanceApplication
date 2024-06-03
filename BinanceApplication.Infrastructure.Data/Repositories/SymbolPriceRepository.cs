using BinanceApplication.Infrastructure.Data;
using BinanceApplication.Infrastructure.DbEntities;
using BinanceApplication.Infrastructure.Repositories;

namespace BinanceApplication.BLL.Repositories
{
    public class SymbolPriceRepository(IdentityCoreDbContext context) : ISymbolPriceRepository
    {
        public async Task<AveragePriceDataResponse> Get24hAveragePrice(string symbol)
        {
            var currentDate = DateTime.UtcNow;
            var startDate = currentDate.Subtract(TimeSpan.FromHours(24));

            decimal? priceFor24Period = context.Set<SymbolsPrice>()
                .Where(x => x.Symbol == symbol
                            && x.EventTime >= startDate
                            && x.EventTime <= currentDate)
                .Average(x => x.Price);

            return new AveragePriceDataResponse(symbol, priceFor24Period);
        }

        public async Task<List<AverageMovingPrice>> GetSimpleMovingAverage(string symbol, List<DateTime> period)
        {
            var result = context.Set<SymbolsPrice>()
                .Where(x => x.Symbol == symbol
                        && period.Contains(x.EventTime.Date))
                .GroupBy(x => x.EventTime)
                .Select(y => new AverageMovingPrice
                {
                    Date = new DateTime(y.Key.Year, y.Key.Month, y.Key.Day),
                    Price = y.Average(x => x.Price)
                })
                .AsEnumerable()
                .DistinctBy(x=> x.Date)
                .ToList();

            return result;
        }

        public async Task BulkInsert(List<SymbolsPrice> symbols)
        {
            context.Set<SymbolsPrice>()
                .AddRangeAsync(symbols);

             context.SaveChangesAsync();
        }
    }
}
