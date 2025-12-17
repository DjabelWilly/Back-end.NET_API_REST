using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await _ratingRepository.GetAllRatings();
        }

        public async Task<Rating?> GetRatingById(int id)
        {
            return await _ratingRepository.GetRatingById(id);
        }

        public async Task<Rating> SaveRating(RatingViewModel vm)
        {
            var entity = _mapper.Map<Rating>(vm);
            await _ratingRepository.SaveRating(entity);
            return entity;
        }

        public async Task<Rating?> UpdateRating(RatingViewModel vm)
        {
            var existing = await _ratingRepository.GetRatingById(vm.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Rating {vm.Id} introuvable.");

            // mappe les valeurs de vm dans l'entity existante dans la db.
            _mapper.Map(vm, existing);

            var updatedRating = await _ratingRepository.Update(existing);

            return updatedRating;
        }

        public async Task DeleteRating(int id)
        {
            var rating = await _ratingRepository.GetRatingById(id);
            if (rating == null)
                throw new KeyNotFoundException($"Rating {id} introuvable.");

            await _ratingRepository.Delete(rating);
        }
    }
}