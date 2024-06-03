using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.Infrastructure.Models;
using BinanceApplication.Infrastructure.Repositories;

namespace BinanceApplication.BLL.Services
{
    public class SymbolPriceService(IConverterService converter,
        ISymbolPriceRepository repository) : ISymbolPriceService
    {
        public async Task<AveragePriceDataResponse> GetAveragePrice(string symbol)
        {
            var averagePrice = await repository.Get24hAveragePrice(symbol);

            return averagePrice;
        }

        public async Task<List<AverageMovingPrice>?> SimpleMovingAverage(SimpleMovingAverageRequest request)
        {
            var period = ParsePeriodToTimeSpam(request.Period);

            if(period == new TimeSpan())
            {
                return [];
            }

            var dates = GetSimpleMovingDates(request.NumberOfDataPoints, period, request.StartTime);

            var simpleMovingAverage = await repository.GetSimpleMovingAverage(request.Symbol, dates);

            return simpleMovingAverage;
        }

        private TimeSpan ParsePeriodToTimeSpam(string period) =>
            period switch
            {
                "1w" => TimeSpan.FromDays(7),
                "1d" => TimeSpan.FromDays(1),
                "30m" => TimeSpan.FromMinutes(30),
                "5m" => TimeSpan.FromMinutes(5),
                "1m" => TimeSpan.FromMinutes(1),
                _ => new TimeSpan()
            };

        private List<DateTime> GetSimpleMovingDates(int numberOfDataPoints, TimeSpan period, DateTime? startTime)
        {
            List<DateTime> dates = new();

            startTime ??= DateTime.UtcNow;

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                var date = startTime.Value - ( i * period);

                dates.Add(date.Date);
            }

            return dates;
        }
    }
}
