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

            if (entity == null)
                throw new ArgumentException("Aucun Id ne correspond");

            return entity;
        }

        // Mise à jour / modification d'un BidList
        public async Task<BidList?> UpdateBidList(BidListViewModel vm)
        {
            if (vm.Id == 0)
                throw new ArgumentException("L'Id est requis pour mettre à jour le BidList.");

            // Récupère l'entité existante
            var existingBidList = await _bidListRepository.GetBidListById(vm.Id);
            if (existingBidList == null)
                throw new KeyNotFoundException($"BidList {vm.Id} introuvable.");

            // Mappe les champs du vm dans entity
            _mapper.Map(vm, existingBidList);

            // Sauvegarde les modifications
          return await _bidListRepository.Update(existingBidList);
        }

        public async Task DeleteBidList(int id)
        {
            var entity = await _bidListRepository.GetBidListById(id);

            if (entity == null)
                throw new KeyNotFoundException($"BidList {id} introuvable.");

            await _bidListRepository.Delete(entity);
        }


    }
}
