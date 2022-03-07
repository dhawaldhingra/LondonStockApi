using LondonStockApi.Repository;

namespace LondonStockApi.Models
{
    /// <summary>
    /// A static cache of all the tickers and broker IDs
    /// </summary>
    internal static class Cache
    {
        private static HashSet<string> _tickers;
        private static HashSet<int> _brokers;

        internal static HashSet<string> Tickers 
        {
            get
            {
                return _tickers;
            }
            set
            {
                if(_tickers==null || _tickers.Count==0)
                    _tickers = value;
            } 
        }
        internal static HashSet<int> BrokerIds
        {
            get
            {
                return _brokers;
            }
            set
            {
                if(_brokers==null || _brokers.Count==0)
                    _brokers = value;
            }
        }
        internal static IStocksRepository repository;
    }
}
