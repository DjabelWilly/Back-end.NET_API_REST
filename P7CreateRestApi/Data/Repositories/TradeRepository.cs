using System;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetAllTrades()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade?> GetTradeById(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task SaveTrade(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrade(Trade trade)
        {
            _context.Trades.Update(trade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrade(Trade trade)
        {
            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();
        }
    }
}
