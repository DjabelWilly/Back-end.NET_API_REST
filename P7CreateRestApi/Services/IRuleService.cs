using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<RuleViewModel>> GetAllRules();
        Task<RuleViewModel?> GetRuleById(int id);
        Task<RuleViewModel> SaveRule(RuleViewModel vm);
        Task<RuleViewModel?> UpdateRule(RuleViewModel vm);
        Task DeleteRule(int id);
    }
}