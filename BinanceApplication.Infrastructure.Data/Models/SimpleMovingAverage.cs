

namespace BinanceApplication.BLL.Models.RequestModels
{
    public record SimpleMovingAverage
    {
        public SimpleMovingAverage() { }

        public SimpleMovingAverage(int numberOfDataPoints,
            TimeSpan period, 
            DateTime startTime) 
        {
            NumberOfDataPoints = numberOfDataPoints;
            Period = period;
            StartTime = startTime;
        }

        public int NumberOfDataPoints { get; init; }

        public TimeSpan Period { get; init; }

        public DateTime StartTime { get; init; }
    }
}
