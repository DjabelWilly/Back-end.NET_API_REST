using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePoints();
        Task<CurvePoint?> GetCurvePointById(int id);
        Task SaveCurvePoint(CurvePoint entity);
        Task Update(CurvePoint entity);
        Task Delete(CurvePoint entity);
    }
}
