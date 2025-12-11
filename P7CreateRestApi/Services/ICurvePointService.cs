using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface ICurvePointService
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePoints();
        Task<CurvePoint?> GetCurvePointById(int id);
        Task<CurvePoint> SaveCurvePoint(CurvePointViewModel vm);
        Task<CurvePoint?> UpdateCurvePoint(CurvePointViewModel vm);
        Task DeleteCurvePoint(int id);
    }
}
