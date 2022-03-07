using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.Models
{
    /// <summary>
    /// Class representing a stock's price for the current day
    /// </summary>
    public class StockPrice
    {
        /// <summary>
        /// The unique ticker of the stock
        /// </summary>
        [Required]
        [MaxLength(8)]
        public string Ticker { get; set; }

        /// <summary>
        /// Current price of the stock
        /// </summary>
        [Required]
        public float Current { get; set; }

        /// <summary>
        /// High price of the stock for the day
        /// </summary>
        [Required]
        public float High { get; set; }

        /// <summary>
        /// Low price of the stock for the dat
        /// </summary>
        [Required]
        public float Low { get; set; }
    }
}
