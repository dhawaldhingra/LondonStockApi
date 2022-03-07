using LondonStockApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.DataModels
{
    /// <summary>
    /// Class representing the exchange of shares notified by a broker
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The unique transaction id
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Unique 3-8 letter ticker of the stock
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// The price at which the stock was traded
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// The total number of stocks traded
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// The ID of the broker who is managing this particular trade
        /// </summary>
        public int BrokerId { get; set; }

        /// <summary>
        /// The transaction ID used by the broker. 
        /// This a a Guid type field but sqlite has support for strings only sodecalring it as a string but adding regex match to the annotations.
        /// </summary>
        public string BrokerTransactionId { get; set; }

        /// <summary>
        /// The date and time of the transaction
        /// </summary>
        public DateTime Date { get; set; }
    }
}
