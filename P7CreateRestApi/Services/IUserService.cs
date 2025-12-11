using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services

{
    public interface IUserService
    {
        Task<User> SaveUser(UserViewModel vm);

        //Task UpdateBidList(BidListViewModel vm);

        //Task<BidList> GetBidId(int id);

        Task DeleteBidList(int id);
    }
}
