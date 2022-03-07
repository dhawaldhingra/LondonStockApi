using LondonStockApi.DataModels;

namespace LondonStockApi.Repository
{
    /// <summary>
    /// Decleration of StocksRepository. Any class handling the database connectivity will have to implement this interface.
    /// </summary>
    public interface IStocksRepository
    {
        Task<IEnumerable<StockPriceDaily>> GetAllPrices();

        Task<StockPriceDaily> GetPrice(string ticker);

        Task<IEnumerable<StockPriceDaily>> GetPrices(IEnumerable<string> tickers);

        Task UpdatePrice(StockPriceDaily stockPrice);

        Task<Transaction> RecordTransaction(Transaction transaction);
        IList<string> GetAllTickers();
        IList<int> GetAllBrokerIds();
    }
}
