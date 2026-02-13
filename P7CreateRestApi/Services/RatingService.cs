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

        public async Task<IEnumerable<RatingViewModel>> GetAllRatings()
        {
            var entities = await _ratingRepository.GetAllRatings();
            return _mapper.Map<IEnumerable<RatingViewModel>>(entities);
        }

        public async Task<RatingViewModel?> GetRatingById(int id)
        {
            var entity = await _ratingRepository.GetRatingById(id);
            return entity == null 
                ? null 
                : _mapper.Map<RatingViewModel>(entity);
        }

        public async Task<RatingViewModel> SaveRating(RatingViewModel vm)
        {
            var entity = _mapper.Map<Rating>(vm);
            await _ratingRepository.SaveRating(entity);
            return _mapper.Map<RatingViewModel>(entity);
        }

        public async Task<RatingViewModel?> UpdateRating(RatingViewModel vm)
        {
            var existing = await _ratingRepository.GetRatingById(vm.Id);
            if (existing == null)
                return null;

            _mapper.Map(vm, existing);

            var updated = await _ratingRepository.Update(existing);

            return _mapper.Map<RatingViewModel>(updated);
        }


        public async Task DeleteRating(int id)
        {
            var entity = await _ratingRepository.GetRatingById(id);
            if (entity == null) return;

            await _ratingRepository.Delete(entity);
        }
    }
}
