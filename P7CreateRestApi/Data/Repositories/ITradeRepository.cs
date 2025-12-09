using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllTrades();
        Task<Trade?> GetTradeById(int id);
        Task SaveTrade(Trade trade);
        Task UpdateTrade(Trade trade);
        Task DeleteTrade(Trade trade);
    }
}

