namespace BinanceApplication.Infrastructure.DbEntities
{
    public sealed class SymbolsPrice
    {
        public Guid Id { get; set; }

        public string Symbol { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset EventTime { get; set; }
    }
}
