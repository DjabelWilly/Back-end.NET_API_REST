using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<Rating?> GetRatingById(int id);
        Task SaveRating(Rating entity);
        Task<Rating?> Update(Rating entity);
        Task Delete(Rating entity);
    }
}
