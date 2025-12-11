using P7CreateRestApi.Entities;
using P7CreateRestApi.Application.ViewModels;

namespace P7CreateRestApi.Services
{
    public interface ITradeService
    {
        Task<IEnumerable<Trade>> GetAllTrades();
        Task<Trade?> GetTradeById(int id);
        Task<Trade> SaveTrade(TradeViewModel vm);
        Task<Trade?> UpdateTrade(TradeViewModel vm);
        Task DeleteTrade(int id);
    }
}
