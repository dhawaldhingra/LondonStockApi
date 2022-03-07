using LondonStockApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.Models
{
    /// <summary>
    /// Class representing the transaction details to be sent by an authorized broker
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The unique transaction id
        /// </summary>
        private int TransactionId { get; set; }

        /// <summary>
        /// Unique 3-8 letter ticker of the stock
        /// </summary>
        [MaxLength(8)]
        [Required]
        [ValidTicker()]
        public string Ticker { get; set; }

        /// <summary>
        /// The price at which the stock was traded
        /// </summary>
        [Required]
        public float Price { get; set; }
        /// <summary>
        /// The total number of stocks traded
        /// </summary>
        [Required]
        public float Quantity { get; set; }

        /// <summary>
        /// The ID of the broker who is managing this particular trade
        /// </summary>
        [Required]
        [ValidBroker]
        public int BrokerId { get; set; }

        /// <summary>
        /// The transaction ID used by the broker. 
        /// This a a Guid type field but sqlite has support for strings only. So, decalring this as a string but adding regex match to the annotations.
        /// The Guid should be in upper case and surrounded in curly braces. E.g. {902C3392-67D7-43EA-A390-01BC5B7FE59E}
        /// </summary>
        [Required]
        [RegularExpression("^{[A-Z0-9]{8}-([A-Z0-9]{4}-){3}[A-Z0-9]{12}}$")]
        public string BrokerTransactionId { get; set; }
    }
}
