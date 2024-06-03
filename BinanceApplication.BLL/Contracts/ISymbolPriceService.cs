using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.Infrastructure.Models;

namespace BinanceApplication.BLL.Contracts;

public interface ISymbolPriceService
{
    Task<AveragePriceDataResponse> GetAveragePrice(string symbol);

    Task<List<AverageMovingPrice>?> SimpleMovingAverage(SimpleMovingAverageRequest request);

}