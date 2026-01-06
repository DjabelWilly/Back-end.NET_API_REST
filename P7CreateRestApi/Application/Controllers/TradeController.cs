using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }


        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTrades()
        {
            var list = await _tradeService.GetAllTrades();
            return Ok(list);
        }


        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var trade = await _tradeService.GetTradeById(id);
            if (trade == null)
                return NotFound();

            return Ok(trade);
        }


        [HttpPost]
        [ProducesResponseType(typeof(TradeViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TradeViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _tradeService.SaveTrade(vm);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] TradeViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (vm.Id != id)
                return BadRequest("Id mismatch");

            var existing = await _tradeService.GetTradeById(id);
            if (existing == null)
                return NotFound();

            await _tradeService.UpdateTrade(vm);

            return Ok(new { message = "update done successfully" });
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _tradeService.GetTradeById(id);
            if (exists == null)
                return NotFound();

            await _tradeService.DeleteTrade(id);
            return Ok(new { message = "delete done successfully" });
        }
    }
}