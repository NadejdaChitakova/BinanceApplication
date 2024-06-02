using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.BLL.Repositories;
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

        public async Task<List<AverageMovingPrice>> SimpleMovingAverage(SimpleMovingAverageRequest request)
        {
            var period = ParsePeriodToTimeSpam(request.Period);

            if(period == new TimeSpan())
            {
                return new List<AverageMovingPrice>();
            }

            var dates = GetSimpleMovingDates(request.NumberOfDataPoints, period, request.StartTime);

            var simpleMovingAverage = await repository.GetSimpleMovingAverage(request.Symbol, dates);

            if (simpleMovingAverage is null)
            {
                return new List<AverageMovingPrice>();
            }

            return simpleMovingAverage;
        }

        private TimeSpan ParsePeriodToTimeSpam(string period)
        {
            switch (period)
            {
                case "1w":
                    return TimeSpan.FromDays(7);
                case "1d":
                    return TimeSpan.FromDays(1);
                case "30m":
                    return TimeSpan.FromMinutes(30);
                case "5m":
                    return TimeSpan.FromMinutes(5);
                case "1m":
                    return TimeSpan.FromMinutes(1);
                default:
                    return new TimeSpan();
            }
        }

        private List<DateTime> GetSimpleMovingDates(int numberOfDataPoints, TimeSpan period, DateTime? startTime)
        {
            List<DateTime> dates = new();

            if (!startTime.HasValue)
            {
                startTime = DateTime.UtcNow;
            }

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                var date = startTime.Value - ( i * period);

                dates.Add(date.Date);
            }

            return dates;
        }
    }
}
