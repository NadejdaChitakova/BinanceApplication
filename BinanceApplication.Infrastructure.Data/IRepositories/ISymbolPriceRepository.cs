using BinanceApplication.BLL.Repositories;
using BinanceApplication.Infrastructure.DbEntities;

namespace BinanceApplication.Infrastructure.Repositories
{
    public interface ISymbolPriceRepository
    {
        Task<AveragePriceDataResponse> Get24hAveragePrice(string symbol);

        Task<List<AverageMovingPrice>> GetSimpleMovingAverage(string symbol, List<DateTime> period);

        Task BulkInsert(List<SymbolsPrice> symbols);
    }
}
