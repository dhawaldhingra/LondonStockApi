using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.DataModels
{
    public class StockPriceDaily
    {
        [Key]
        public string Ticker { get; set; }
        public float Current { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
    }
}
