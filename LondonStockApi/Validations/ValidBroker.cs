using LondonStockApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LondonStockApi.Validations
{
    /// <summary>
    /// Class responsible for validating a broker id
    /// </summary>
    public class ValidBroker : ValidationAttribute
    {
        protected override ValidationResult IsValid(object brokerId, ValidationContext validationContext)
        {
            // This works on assumption/logic that we've all the valid brokers are present in the DB. 
            // So, if a broker id passed in the input is present in the DB, it'll be a valid broker. If not present in the DB, it'll be an invalid broker.
            if (Cache.BrokerIds.Contains(Convert.ToInt32(brokerId)))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid Broker ID Supplied");
            }
        }
    }
}
