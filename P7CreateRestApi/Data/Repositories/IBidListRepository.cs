using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface IBidListRepository
    {
        Task SaveBidList(BidList entity);

        Task<BidList?> GetBidListById(int id);

        Task Update(BidList entity);

        Task Delete(BidList entity);
    }
}
