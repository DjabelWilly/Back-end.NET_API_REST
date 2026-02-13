using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface ICurvePointService
    {
        Task<IEnumerable<CurvePointViewModel>> GetAllCurvePoints();
        Task<CurvePointViewModel?> GetCurvePointById(int id);
        Task<CurvePointViewModel> SaveCurvePoint(CurvePointViewModel vm);
        Task<CurvePointViewModel?> UpdateCurvePoint(CurvePointViewModel vm);
        Task DeleteCurvePoint(int id);
    }
}
