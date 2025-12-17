using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<Rating?> GetRatingById(int id);
        Task<Rating> SaveRating(RatingViewModel vm);
        Task<Rating?> UpdateRating(RatingViewModel vm);
        Task DeleteRating(int id);
    }
}