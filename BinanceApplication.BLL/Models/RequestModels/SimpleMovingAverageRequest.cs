namespace BinanceApplication.BLL.Models.RequestModels
{
    public record SimpleMovingAverageRequest
    {
        public string Symbol { get; init; }

        public int NumberOfDataPoints { get; init; }

        public string Period { get; init; }

        public DateTime? StartTime { get; init; }
    }
}
