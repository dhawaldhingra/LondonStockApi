//using LondonStockApi.DataModels;
using AutoMapper;
using LondonStockApi.BindingModels;
using LondonStockApi.Models;
using LondonStockApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LondonStockApi.Controllers
{
    /// <summary>
    /// API for getting and updating stock prices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]

    public class StocksController : ControllerBase
    {
        private readonly IStocksRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly object _padLock;

        /// <summary>
        /// Initializes a new instance of StocksController class.
        /// </summary>
        /// <param name="stockRepository">Object for handling database queries</param>
        /// <param name="mapper">Object for mapping between DB objects and API classes</param>
        public StocksController(IStocksRepository stockRepository, IMapper mapper)
        {
            // Dependency injection of database handler and mapper
            _stockRepository = stockRepository;
            _mapper = mapper;
            
            // Get all the tickers and brokerIds and store in the cache so we don't end up making a database call for every request.
            // This is being cached because the list of brokers and tickers is expected to not change.
            var tickers = _stockRepository.GetAllTickers();
            var brokerIds = _stockRepository.GetAllBrokerIds();
            Cache.Tickers = new HashSet<string>(tickers);
            Cache.BrokerIds = new HashSet<int>(brokerIds);

            // Initialise the lock for performing thread-safe transactions such as updating the price
            _padLock = new object();
        }

        /// <summary>
        /// Gets price information of a single stock specified by its ticker
        /// </summary>
        /// <param name="ticker">The ticker symbol for which price is requested</param>
        /// <returns>Http result containing the price info if successfull. Otherwise the relevant error message.</returns>
        [HttpGet("{ticker}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockPrice>> GetSingle(string ticker)
        {
            var price =  await _stockRepository.GetPrice(ticker);

            var temp = _mapper.Map<StockPrice>(price);

            if (price == null)
                return NotFound($"No price information was found for the ticker {ticker}. Possible reason could be invalid ticker was supplied."); 
            else
                return _mapper.Map<StockPrice>(await _stockRepository.GetPrice(ticker));
        }

        /// <summary>
        /// Returns the prices of all the stocks
        /// </summary>
        /// <returns>Today's prices of all the stocks</returns>
        /// <remarks>This method does not support pagination and therefore may return large amount of data.</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<StockPrice>> GetPrices()
        {
            return _mapper.Map<List<StockPrice>>( await _stockRepository.GetAllPrices());
        }

        /// <summary>
        /// Gets the prices of the stocks for the tickers specified in the input
        /// </summary>
        /// <param name="tickers">Comma separated ticker symbols</param>
        /// <returns>Price details for the requested tickers</returns>
        /// <remarks>
        /// The tickers should be passed as a comma separated list for this method
        /// </remarks>
        [HttpGet("GetMultiple/{tickers}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetPrices([FromRoute]
        [ModelBinder(BinderType = typeof(CommaDelimitedListModelBinder))]
        IEnumerable<string> tickers)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<StockPrice>>(await _stockRepository.GetPrices(tickers));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.ToString());
            }
        }

        /// <summary>
        /// Receives transactions from brokers and updates the price of the specified stock
        /// </summary>
        /// <param name="transaction">Transaction details</param>
        /// <returns>Http response indicating whether or not the request was successfully processed</returns>
        /// <remarks>
        /// This is a put request as opposed to a post request because the main role of this operation is to update the current price of the stock.
        /// Adding the transaction into the TransactionHistory table is a secondary NFR it is performing.
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostTransaction([FromBody] Transaction transaction)
        {
            // On receiving a new trade deal information from the brokers, we need to perform these two things-
            // 1. Update the Current, High & Low Prices
            // 2. Record an entry in the TransactionHistory table

            try
            {
                // Transaction sensitive area. We don't want multiple requests to update/overwrite each other's changes
                lock (_padLock)
                {
                    // The transaction received from the broker only contains the current price.
                    // We need to check if this is will result in updating the high/low price of the stock. For this, we need to fetch the existing price details.

                    // Get existing price from the DB
                    var priceDetailsTask = _stockRepository.GetPrice(transaction.Ticker);

                    // The DB read is Async, so wait for the task to complete.
                    priceDetailsTask.Wait();

                    // Once the task completes, extract results from the task
                    var priceDetails = priceDetailsTask.Result;

                    // Update the current, high and low prices
                    priceDetails.Current = transaction.Price;
                    priceDetails.High = Math.Max(priceDetails.High, transaction.Price);
                    priceDetails.Low = Math.Min(priceDetails.Low, transaction.Price);

                    // Lastly, update the database table
                    var response = _stockRepository.UpdatePrice(priceDetails);
                }
            }
            catch (Exception ex)
            {
                // If an error ocurred during updating the database, return response to client and don't log an entry in the transanction history table
                return Problem(detail: ex.Message);
            }

            // Now record the entry in the transaction history table
            try
            {
                var transactionUpdate = _mapper.Map<DataModels.Transaction>(transaction);
                transactionUpdate.Date = DateTime.Now;
                var result = await _stockRepository.RecordTransaction(_mapper.Map<DataModels.Transaction>(transaction));
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

    }
}
