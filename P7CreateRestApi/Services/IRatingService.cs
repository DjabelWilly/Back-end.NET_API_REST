using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingViewModel>> GetAllRatings();
        Task<RatingViewModel?> GetRatingById(int id);
        Task<RatingViewModel> SaveRating(RatingViewModel vm);
        Task<RatingViewModel?> UpdateRating(RatingViewModel vm);
        Task DeleteRating(int id);
    }
}