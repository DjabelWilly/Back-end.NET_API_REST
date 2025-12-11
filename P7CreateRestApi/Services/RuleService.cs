using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public RuleService(IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _ruleRepository.GetAllRules();
        }

        public async Task<Rule?> GetRuleById(int id)
        {
            return await _ruleRepository.GetRuleById(id);
        }

        public async Task<Rule> SaveRule(RuleViewModel vm)
        {
            var entity = _mapper.Map<Rule>(vm);
            await _ruleRepository.SaveRule(entity);
            return entity;
        }

        public async Task<Rule?> UpdateRule(RuleViewModel vm)
        {
            var existing = await _ruleRepository.GetRuleById(vm.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Rule {vm.Id} introuvable.");

           _mapper.Map(vm, existing);

            await _ruleRepository.Update(existing);

            return existing;
        }

        public async Task DeleteRule(int id)
        {
            var rule = await _ruleRepository.GetRuleById(id);
            if (rule == null)
                throw new KeyNotFoundException($"Rule {id} introuvable.");

            await _ruleRepository.Delete(rule);
        }
    }
}
