using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("trade")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // -------------------- GET ALL --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTrades()
        {
            var list = await _tradeService.GetAllTrades();
            return Ok(list);
        }

        // -------------------- GET BY ID --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TradeViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var trade = await _tradeService.GetTradeById(id);
            if (trade == null)
                return NotFound(new { message = $"Trade avec Id={id} non trouvée." });

            return Ok(trade);
        }

        // -------------------- CREATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(TradeViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TradeViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides." });

            var created = await _tradeService.SaveTrade(vm);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // -------------------- UPDATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TradeViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] TradeViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides." });

            if (vm.Id != id || vm.Id == 0)
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID fourni." });
            
            var exists = await _tradeService.GetTradeById(id);

            if (exists == null)
                return NotFound(new { message = $"Trade avec Id={id} non trouvée." });
            
            var updated = await _tradeService.UpdateTrade(vm);

            return Ok(updated);
        }

        // -------------------- DELETE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _tradeService.GetTradeById(id);
            if (exists == null)
                return NotFound(new { message = $"Trade avec Id={id} non trouvée." });

            await _tradeService.DeleteTrade(id);

            return NoContent();
        }
    }
}