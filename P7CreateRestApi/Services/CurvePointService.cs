using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;
        private readonly IMapper _mapper;

        public CurvePointService(ICurvePointRepository curvePointRepository, IMapper mapper)
        {
            _curvePointRepository = curvePointRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllCurvePoints()
        {
            return await _curvePointRepository.GetAllCurvePoints();
        }

        public async Task<CurvePoint?> GetCurvePointById(int id)
        {
            var entity = await _curvePointRepository.GetCurvePointById(id);
            if (entity == null)
                throw new KeyNotFoundException($"CurvePoint {id} introuvable.");

            return entity;
        }

        public async Task<CurvePoint> SaveCurvePoint(CurvePointViewModel vm)
        {
            var entity = _mapper.Map<CurvePoint>(vm);
            await _curvePointRepository.SaveCurvePoint(entity);
            return entity;
        }

        public async Task<CurvePoint?> UpdateCurvePoint(CurvePointViewModel vm)
        {
            if (vm.Id == 0)
                throw new ArgumentException("L'Id est requis pour mettre à jour le CurvePoint.");

            var existing = await _curvePointRepository.GetCurvePointById(vm.Id);
            if (existing == null)
                throw new KeyNotFoundException($"CurvePoint {vm.Id} introuvable.");

            _mapper.Map(vm, existing);

            await _curvePointRepository.Update(existing);
            return existing;
        }

        public async Task DeleteCurvePoint(int id)
        {
            var entity = await _curvePointRepository.GetCurvePointById(id);
            if (entity == null)
                throw new KeyNotFoundException($"CurvePoint {id} introuvable.");

            await _curvePointRepository.Delete(entity);
        }
    }
}
