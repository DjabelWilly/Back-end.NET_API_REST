using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("bidlist")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;

        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
        }

        // -------------------- CREATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides" });

            var created = await _bidListService.SaveBidList(vm);
            return CreatedAtAction(nameof(GetBidById), new { id = created.Id }, created);
        }

        // -------------------- GET BY ID --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBidById(int id)
        {
            var bid = await _bidListService.GetBidId(id);
            if (bid == null)
                return NotFound(new { message = $"BidList avec Id={id} non trouvée." });

            return Ok(bid);
        }

        // -------------------- UPDATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Données invalides" });

            if (id != vm.Id || vm.Id == 0)
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet fourni." });

            // Vérification existence avant l'update
            var exists = await _bidListService.GetBidId(id);
            if (exists == null)
                return NotFound(new { message = $"BidList avec Id={id} non trouvée." });

            var updated = await _bidListService.UpdateBidList(vm);
            return Ok(updated);
        }

        // -------------------- DELETE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var exists = await _bidListService.GetBidId(id);
            if (exists == null)
                return NotFound(new { message = $"BidList avec Id={id} non trouvée." });

            await _bidListService.DeleteBidList(id);
            return NoContent();
        }
    }
}
