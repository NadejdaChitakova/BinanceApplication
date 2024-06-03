namespace BinanceApplication.BLL.Repositories;

public class AveragePriceDataResponse 
{
    public AveragePriceDataResponse() { }

    public AveragePriceDataResponse(string Symbol, decimal? AveragePrice)
    {
        this.Symbol = Symbol;
        this.AveragePrice = AveragePrice;
    }

    public string Symbol { get; set; }
    public decimal? AveragePrice { get; set; }
};