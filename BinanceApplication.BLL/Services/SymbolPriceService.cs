using BinanceApplication.BLL.Contracts;
using BinanceApplication.Infrastructure.Repositories;

namespace BinanceApplication.BLL.Services
{
    public class SymbolPriceService(IConverterService converter,
        ISymbolPriceRepository repository) : ISymbolPriceService
    {
        private readonly string[] symbols = { "btcusdt@ticker", "adausdt@ticker", "ethusdt@ticker" };
        private const int MAX_BUFFER_SIZE = 5000;

        public Task GetAveragePrice()
        {
            throw new NotImplementedException();
        }
    }
}
