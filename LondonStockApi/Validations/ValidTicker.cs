using LondonStockApi.Models;
using LondonStockApi.Repository;
using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.Validations
{
    /// <summary>
    /// Class responsible for validating a ticker symbol.
    /// </summary>
    public class ValidTicker : ValidationAttribute
    {
        protected override ValidationResult IsValid(object ticker, ValidationContext validationContext)
        {
            // This works on assumption/logic that we've all the valid tickers present in the DB. 
            // So, if a ticker passed in the input is present in the DB, it'll be a valid ticker. If not present in the DB, it'll be an invalid ticker.
            if(Cache.Tickers.Contains(ticker))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid Ticker Supplied");
            }
        }
    }
}
