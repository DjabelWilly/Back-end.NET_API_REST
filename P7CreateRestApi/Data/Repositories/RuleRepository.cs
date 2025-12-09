using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly LocalDbContext _context;

        public RuleRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<Rule?> GetRuleById(int id)
        {
            return await _context.Rules.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveRule(Rule entity)
        {
            _context.Rules.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Rule entity)
        {
            _context.Rules.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Rule entity)
        {
            _context.Rules.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
