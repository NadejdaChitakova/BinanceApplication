using BinanceApplication.Infrastructure.DbEntities;

namespace BinanceApplication.BLL.Contracts
{
    public interface IConverterService
    {
SymbolsPrice ConvertStringToSymbolPriceEntity(string value);
    }
}
