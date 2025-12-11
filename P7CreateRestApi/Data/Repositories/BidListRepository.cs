using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;


namespace P7CreateRestApi.Data.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _context;

        public BidListRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task SaveBidList(BidList entity)
        {
            _context.BidLists.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BidList?> GetBidListById(int id)
        {
            return await _context.BidLists
              .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BidList?> Update(BidList entity)
        {
            _context.BidLists.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(BidList entity)
        {
            _context.BidLists.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
