using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _context;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating?> GetRatingById(int id)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveRating(Rating entity)
        {
            _context.Ratings.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Rating?> Update(Rating entity)
        {
            _context.Ratings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Rating entity)
        {
            _context.Ratings.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}