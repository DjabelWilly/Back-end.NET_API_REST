using P7CreateRestApi.Entities;
using P7CreateRestApi.Application.ViewModels;

namespace P7CreateRestApi.Services
{
    public interface ITradeService
    {
        Task<IEnumerable<TradeViewModel>> GetAllTrades();
        Task<TradeViewModel?> GetTradeById(int id);
        Task<TradeViewModel> SaveTrade(TradeViewModel vm);
        Task<TradeViewModel?> UpdateTrade(TradeViewModel vm);
        Task DeleteTrade(int id);
    }

}