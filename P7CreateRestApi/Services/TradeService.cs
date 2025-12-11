using AutoMapper;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Application.ViewModels;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repository;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Trade>> GetAllTrades()
        {
            return await _repository.GetAllTrades();
        }

        public async Task<Trade?> GetTradeById(int id)
        {
            return await _repository.GetTradeById(id);
        }

        public async Task<Trade> SaveTrade(TradeViewModel vm)
        {
            var entity = _mapper.Map<Trade>(vm);
            await _repository.SaveTrade(entity);
            return entity;
        }

        public async Task<Trade?> UpdateTrade(TradeViewModel vm)
        {
            var existing = await _repository.GetTradeById(vm.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Trade {vm.Id} introuvable.");

            _mapper.Map(vm, existing);
            await _repository.UpdateTrade(existing);
            return existing;
        }

        public async Task DeleteTrade(int id)
        {
            var existing = await _repository.GetTradeById(id);
            if (existing == null)
                throw new KeyNotFoundException($"Trade {id} introuvable.");

            await _repository.DeleteTrade(existing);
        }
    }
}
