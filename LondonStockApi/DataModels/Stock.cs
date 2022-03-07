namespace LondonStockApi.DataModels
{
    /// <summary>
    /// Class representing a stock
    /// </summary>
    public class Stock
    {
        public int StockId { get; set; }
        public string StockName { get; set;}
        public string Ticker { get; set; }
    }
}
