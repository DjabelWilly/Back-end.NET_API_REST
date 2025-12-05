using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;
using Dot.Net.WebApi.Data;

namespace P7CreateRestApi.Data.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllCurvePoints()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> GetCurvePointById(int id)
        {
            return await _context.CurvePoints.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveCurvePoint(CurvePoint entity)
        {
            _context.CurvePoints.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CurvePoint entity)
        {
            _context.CurvePoints.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CurvePoint entity)
        {
            _context.CurvePoints.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
