using BinanceApplication.BLL.Models.RequestModels;
using BinanceApplication.BLL.Repositories;

namespace BinanceApplication.BLL.Contracts;

public interface ISymbolPriceService
{
    Task<AveragePriceDataResponse> GetAveragePrice(string symbol);

    Task<List<AverageMovingPrice>?> SimpleMovingAverage(SimpleMovingAverageRequest request);

}