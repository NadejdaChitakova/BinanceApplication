using Binance.Spot;

namespace BinanceApplication
{
    public class SocketClient
    {
        private readonly string baseUrl = "wss://stream.binance.com:443/ws/"; // stream?streams=

        private readonly string[] symbols = { "BTCUSDT", "ADAUSDT", "ETHUSDT" };

        public async Task OpenConnection()
        {
         var combinedStream = string.Join("/", symbols);
         var url = string.Concat(baseUrl, combinedStream);


         var websocket = new MarketDataWebSocket("BTCUSDT");

         websocket.OnMessageReceived(
                                     (data) =>
                                     {
                                         Console.WriteLine(data);

                                         return Task.CompletedTask;
                                     }, CancellationToken.None);

         await websocket.ConnectAsync(CancellationToken.None);

        }
        }
    }

