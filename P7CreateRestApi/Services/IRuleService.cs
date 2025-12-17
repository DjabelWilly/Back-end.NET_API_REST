using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> GetAllRules();
        Task<Rule?> GetRuleById(int id);
        Task<Rule> SaveRule(RuleViewModel vm);
        Task<Rule?> UpdateRule(RuleViewModel vm);
        Task DeleteRule(int id);
    }
}