using AutoMapper;
using LondonStockApi.DataModels;
using LondonStockApi.Models;

namespace LondonStockApi.Profiles
{
    /// <summary>
    /// Class for auto-mapping between API Class Objects and Database Class Objects
    /// </summary>
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<StockPrice, StockPriceDaily>().ReverseMap();
            CreateMap<DataModels.Transaction, Models.Transaction>().ReverseMap();
        }
    }
}
