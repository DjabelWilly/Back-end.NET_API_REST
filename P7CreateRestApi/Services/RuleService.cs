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

        public async Task<IEnumerable<RuleViewModel>> GetAllRules()
        {
            {
                var entities = await _ruleRepository.GetAllRules();
                return _mapper.Map<IEnumerable<RuleViewModel>>(entities);
            }
        }

        public async Task<RuleViewModel?> GetRuleById(int id)
        {
            var entity = await _ruleRepository.GetRuleById(id);
            return entity == null 
                ? null 
                : _mapper.Map<RuleViewModel>(entity);
        }

        public async Task<RuleViewModel> SaveRule(RuleViewModel vm)
        {
            var entity = _mapper.Map<Rule>(vm);
            await _ruleRepository.SaveRule(entity);
            return _mapper.Map<RuleViewModel>(entity);
        }

        public async Task<RuleViewModel?> UpdateRule(RuleViewModel vm)
        {
            var existing = await _ruleRepository.GetRuleById(vm.Id);
            if (existing == null)
                return null;

            _mapper.Map(vm, existing);

            var updated = await _ruleRepository.Update(existing);

            return _mapper.Map<RuleViewModel>(updated);
        }

        public async Task DeleteRule(int id)
        {
            var entity = await _ruleRepository.GetRuleById(id);
            if (entity == null) return;

            await _ruleRepository.Delete(entity);
        }


    }
}