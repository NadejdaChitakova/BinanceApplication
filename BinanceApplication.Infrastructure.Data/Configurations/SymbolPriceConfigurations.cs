using BinanceApplication.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BinanceApplication.Infrastructure.Configurations
{
    internal sealed class SymbolPriceConfigurations : IEntityTypeConfiguration<SymbolsPrice>
    {
        public void Configure(EntityTypeBuilder<SymbolsPrice> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Price)
                .HasPrecision(20, 10);

        }
    }
}
