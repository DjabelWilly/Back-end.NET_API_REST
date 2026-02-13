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

        public async Task<IEnumerable<CurvePointViewModel>> GetAllCurvePoints()
        {
            var entities = await _curvePointRepository.GetAllCurvePoints();
            return _mapper.Map<IEnumerable<CurvePointViewModel>>(entities);
        }


        public async Task<CurvePointViewModel?> GetCurvePointById(int id)
        {
            var entity = await _curvePointRepository.GetCurvePointById(id);
            return entity == null ? null : _mapper.Map<CurvePointViewModel>(entity);
        }

        public async Task<CurvePointViewModel> SaveCurvePoint(CurvePointViewModel vm)
        {
            var entity = _mapper.Map<CurvePoint>(vm);
            await _curvePointRepository.SaveCurvePoint(entity);
            return _mapper.Map<CurvePointViewModel>(entity);
        }

        public async Task<CurvePointViewModel?> UpdateCurvePoint(CurvePointViewModel vm)
        {
            var existing = await _curvePointRepository.GetCurvePointById(vm.Id);
            if (existing == null) return null;

            _mapper.Map(vm, existing);
            var updated = await _curvePointRepository.Update(existing);
            return _mapper.Map<CurvePointViewModel>(updated);
        }

        public async Task DeleteCurvePoint(int id)
        {
            var entity = await _curvePointRepository.GetCurvePointById(id);
            if (entity == null) return;

            await _curvePointRepository.Delete(entity);
        }
    }
}
