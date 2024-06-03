using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BinanceApplication.Infrastructure.Data
{
    public class IdentityCoreDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public IdentityCoreDbContext() { }

        public IdentityCoreDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
_configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityCoreDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString =
            //    _configuration.GetConnectionString("Database") ??
            //    throw new ArgumentNullException(nameof(_configuration));

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=binanceApp; user id=sa; password=Na!12345678;  TrustServerCertificate=True;");
        }
    }
}
