using Microsoft.EntityFrameworkCore;

namespace LondonStockApi.DataModels
{
    public class StocksDbContext : DbContext
    {
        public StocksDbContext(DbContextOptions<StocksDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<StockPriceDaily> StockPrice_Daily { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> TransactionHistory { get; set; }

        public DbSet<Broker> Brokers { get; set; }
    }
}
