using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services

{
    public interface IBidListService
    {
        Task<BidList> SaveBidList(BidListViewModel vm);

        Task<BidList?> UpdateBidList(BidListViewModel vm);

        Task<BidList?> GetBidId(int id);

        Task DeleteBidList(int id);
    }

}
