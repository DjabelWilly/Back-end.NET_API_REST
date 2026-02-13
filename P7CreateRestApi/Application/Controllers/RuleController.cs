using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("rule")]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }


        //------------- GET ALL -----------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ruleService.GetAllRules();
            return Ok(list);
        }

        //--------------- GET BY ID ---------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RuleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var rule = await _ruleService.GetRuleById(id);
            if (rule == null)
                return NotFound(new { message = $"Rule avec Id={id} non trouvée." });

            return Ok(rule);
        }

        //-------------- CREATE ---------------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(RuleViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RuleViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides." });

            var created = await _ruleService.SaveRule(vm);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //------------------- UPDATE --------------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RuleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] RuleViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides." });

            if (vm.Id != id || vm.Id == 0)
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID fourni." });
            
            var exists = await _ruleService.GetRuleById(id);

            if ( exists == null)
                return NotFound(new { message = $"Rule avec Id={id} non trouvée." });
            
            var updated = await _ruleService.UpdateRule(vm);

            return Ok(updated);
        }

        //------------------- DELETE -----------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _ruleService.GetRuleById(id);
            if (exists == null)
                return NotFound(new { message = $"Rule avec Id={id} non trouvée." });

            await _ruleService.DeleteRule(id);

            return NoContent(); 
        }
    }
}