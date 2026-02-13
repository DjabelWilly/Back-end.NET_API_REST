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

        public async Task<IEnumerable<TradeViewModel>> GetAllTrades()
        {
            var list = await _repository.GetAllTrades();
            return _mapper.Map<IEnumerable<TradeViewModel>>(list);
        }

        public async Task<TradeViewModel?> GetTradeById(int id)
        {
            var entity = await _repository.GetTradeById(id);
            return entity == null
                ? null
                : _mapper.Map<TradeViewModel>(entity);
        }

        public async Task<TradeViewModel> SaveTrade(TradeViewModel vm)
        {
            var entity = _mapper.Map<Trade>(vm);
            await _repository.SaveTrade(entity);
            return _mapper.Map<TradeViewModel>(entity);
        }

        public async Task<TradeViewModel?> UpdateTrade(TradeViewModel vm)
        {
            var existing = await _repository.GetTradeById(vm.Id);
            if (existing == null) return null;

            _mapper.Map(vm, existing);
            var updated = await _repository.UpdateTrade(existing);
            return _mapper.Map<TradeViewModel>(updated);
        }

        public async Task DeleteTrade(int id)
        {
            var existing = await _repository.GetTradeById(id);
            if (existing == null) return;

            await _repository.DeleteTrade(existing);
        }
    }
}