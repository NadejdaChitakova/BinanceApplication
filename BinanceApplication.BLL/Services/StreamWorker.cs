using Binance.Spot;
using BinanceApplication.BLL.Contracts;
using BinanceApplication.BLL.Models;
using BinanceApplication.Infrastructure.Data;
using BinanceApplication.Infrastructure.DbEntities;
using BinanceApplication.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BinanceApplication.BLL.Services
{
    public sealed class StreamWorker(
        IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private readonly string[] symbols = { "btcusdt@ticker", "adausdt@ticker", "ethusdt@ticker" };
        private const int MAX_BUFFER_SIZE = 20;
        private ISymbolPriceRepository _priceRepository;
        private IConverterService _converterService;
        private IdentityCoreDbContext _dbContext;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            BackgroundProcessing();

            _ = GetStreamData();

            return Task.CompletedTask;
        }

        private async ValueTask BackgroundProcessing()
        {
            using var scope = serviceScopeFactory.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<IdentityCoreDbContext>();

            _priceRepository = scope.ServiceProvider.GetRequiredService<ISymbolPriceRepository>();
            _converterService = scope.ServiceProvider.GetRequiredService<IConverterService>();


        }

        public async Task GetStreamData()
        {
            List<string> buffer = new();
            List<string> symbolsData = new();

            await foreach (var data in GetDataAsync())
            {
                buffer.Add(data);

                if (buffer.Count > MAX_BUFFER_SIZE)
                {

                    symbolsData.AddRange(buffer);
buffer.Clear();

                    SaveData(symbolsData);
                    symbolsData.Clear();
                    //ClearBuffer(ref buffer);
                }

                Console.WriteLine(data);
            }

        }

        private async IAsyncEnumerable<string> GetDataAsync()
        {
            var websocket = new MarketDataWebSocket(symbols);
            var channel = new Channel<string>();

            await websocket.ConnectAsync(CancellationToken.None);

            websocket.OnMessageReceived(
                                        async (data) =>
                                        {
                                            var message = data.Remove(data.IndexOf('\0'));
                                            await channel.Writer.WriteAsync(message);
                                        }, CancellationToken.None);

            await foreach (var data in channel.Reader.ReadAllAsync())
            {
                yield return data;
            }
        }

        public void SaveData(List<string> buffer)
        {
            List<SymbolsPrice> symbols = new();

            buffer.ForEach(x =>
            {
                var convertedSymbol = _converterService.ConvertStringToSymbolPriceEntity(x);
                convertedSymbol.Id = Guid.NewGuid();
                symbols.Add(convertedSymbol);
            });

            using var scope = serviceScopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ISymbolPriceRepository>();

            repo.BulkInsert(symbols);
            //_priceRepository.BulkInsert(symbols);
        }

        public void ClearBuffer(ref List<string> buffer)
        {
            buffer.Clear();
        }
    }
}
