using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services

{
    public interface IBidListService
    {
        Task<BidListViewModel> SaveBidList(BidListViewModel vm);
        Task<BidListViewModel?> GetBidId(int id);
        Task<BidListViewModel?> UpdateBidList(BidListViewModel vm);
        Task DeleteBidList(int id);
    }

}
