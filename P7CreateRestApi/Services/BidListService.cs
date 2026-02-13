using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _bidListRepository;
        private readonly IMapper _mapper;

        public BidListService(IBidListRepository bidListRepository, IMapper mapper)
        {
            _bidListRepository = bidListRepository;
            _mapper = mapper;
        }

        public async Task<BidListViewModel> SaveBidList(BidListViewModel vm)
        {
            var entity = _mapper.Map<BidList>(vm); // convertit vm en entity

            await _bidListRepository.SaveBidList(entity); // appele le repo pour persister entity

            return _mapper.Map<BidListViewModel>(entity); // convertit entity en vm et retourne vm  
        }

        public async Task<BidListViewModel?> GetBidId(int id)
        {
            var entity = await _bidListRepository.GetBidListById(id);

            return entity == null
                ? null
                : _mapper.Map<BidListViewModel>(entity);
        }

        public async Task<BidListViewModel?> UpdateBidList(BidListViewModel vm)
        {
            var entity = await _bidListRepository.GetBidListById(vm.Id);

            if (entity == null)
                return null;

            _mapper.Map(vm, entity); // convertit vm sur l'objet entity existant

            var updated = await _bidListRepository.Update(entity);

            return _mapper.Map<BidListViewModel>(updated);  // convertit entity updated en vm et retourne vm
        }

        public async Task DeleteBidList(int id)
        {
            var entity = await _bidListRepository.GetBidListById(id);

            if (entity == null)
                return;

            await _bidListRepository.Delete(entity);
        }
    }
}

