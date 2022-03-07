using LondonStockApi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LondonStockApi.Repository
{
    /// <summary>
    /// Implementation of StocksRepository (EntityFramework to connect to sqllite database)
    /// </summary>
    public class StocksRepository : IStocksRepository
    {
        private readonly StocksDbContext _dbContext;

        public StocksRepository(StocksDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Returns the prices of all the stocks in the DB
        /// </summary>
        /// <returns>Prices of all the sticks</returns>
        public async Task<IEnumerable<StockPriceDaily>> GetAllPrices()
        {
            return  _dbContext.StockPrice_Daily.ToList();
        }

        /// <summary>
        /// Returns the price for the stock by its ticker
        /// </summary>
        /// <param name="ticker">The ticker whose price needs to returned</param>
        /// <returns>The price for a given stock</returns>
        public async Task<StockPriceDaily> GetPrice(string ticker)
        {
            return await _dbContext.FindAsync<StockPriceDaily>(ticker);
        }

        /// <summary>
        /// Returns a list of all the tickers.
        /// </summary>
        /// <returns>A list of all the tickers</returns>
        public IList<string> GetAllTickers()
        {
            return _dbContext.Stocks.Select(x => x.Ticker).ToList();
        }

        /// <summary>
        /// Gets the prices of a given list of tickers
        /// </summary>
        /// <param name="tickers">The tickers whose prices need to be returned</param>
        /// <returns>List of stock prices</returns>
        public async Task<IEnumerable<StockPriceDaily>> GetPrices(IEnumerable<string> tickers)
        {
            return await _dbContext.StockPrice_Daily.Where(p => tickers.Contains(p.Ticker)).ToListAsync();
        }

        /// <summary>
        /// Updates the price of a given stock in the StockPrice_Daily table
        /// </summary>
        /// <param name="stockPrice">The high, low and current prices of a given stock</param>
        /// <returns>Async task result</returns>
        public async Task UpdatePrice(StockPriceDaily stockPrice)
        {
            _dbContext.Entry(stockPrice).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Puts a transaction received from an authroized broker into the table
        /// </summary>
        /// <param name="transaction">The transaction containing information such as stock, quantity and price</param>
        /// <returns>The same transaction as a confirmation of request being processed</returns>
        public async Task<Transaction> RecordTransaction(Transaction transaction)
        {
            _dbContext.TransactionHistory.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        /// <summary>
        /// Returns a list of all the broker Ids present in the DB
        /// </summary>
        /// <returns>List of all the broker Ids</returns>
        public IList<int> GetAllBrokerIds()
        {
            return _dbContext.Brokers.Select(x => x.BrokerId).ToList();
        }
    }
}
