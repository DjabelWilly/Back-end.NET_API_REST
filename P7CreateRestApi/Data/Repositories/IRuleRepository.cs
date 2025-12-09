using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> GetAllRules();
        Task<Rule?> GetRuleById(int id);
        Task SaveRule(Rule entity);
        Task Update(Rule entity);
        Task Delete(Rule entity);
    }
}
