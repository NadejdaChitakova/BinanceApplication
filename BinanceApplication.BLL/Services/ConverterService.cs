using BinanceApplication.BLL.Contracts;
using BinanceApplication.Infrastructure.DbEntities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceApplication.BLL.Services
{
    public class ConverterService() : IConverterService
    {
        public SymbolsPrice ConvertStringToSymbolPriceEntity(string value)
        {
            SymbolsPrice result = new();

            try
            {
using JsonDocument document = JsonDocument.Parse(value);
JsonElement dataElement = document.RootElement.GetProperty("data");
                
                var data = JsonSerializer.Deserialize<Test>(dataElement.GetRawText(), new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                });

                result.Price = data.c;
                result.Symbol = data.s;
                    result.EventTime = DateTimeOffset.FromUnixTimeMilliseconds(data.E);
                
            }
            catch (JsonException ex)
            {
                throw new Exception();
            }

            return result;
        }
    }

    public class Test
    {
        public decimal c { get; set; }
        public string s { get; set; }
        public long E { get; set; }
    }
}
