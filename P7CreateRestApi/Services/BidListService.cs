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


        // Récupère le ViewModel (BidListViewModel) depuis le controller et le convertit en Entity (BidList)
        public async Task<BidList> SaveBidList(BidListViewModel vm)
        {
            var entity = _mapper.Map<BidList>(vm);
            await _bidListRepository.SaveBidList(entity);

            return entity;
        }

        // Get(id)
        public async Task<BidList?> GetBidId(int id)
        {
            var entity = await _bidListRepository.GetBidListById(id);

            return entity;
        }

        // Mise à jour / modification d'un BidList
        public async Task<BidList?> UpdateBidList(BidListViewModel vm)
        {
            var entity = await _bidListRepository.GetBidListById(vm.Id);

            if (entity == null)
                return null;

            _mapper.Map(vm, entity);

            return await _bidListRepository.Update(entity);
        }

        // Suppression
        public async Task DeleteBidList(int id)
        {
            var entity = await _bidListRepository.GetBidListById(id);

            if (entity == null)
                return;

            await _bidListRepository.Delete(entity);
           
        }

    }
}
