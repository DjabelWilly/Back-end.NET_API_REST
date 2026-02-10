using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListService _bidListService;

        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
        }

        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _bidListService.SaveBidList(vm);
            return CreatedAtAction(nameof(GetBidById), new { id = created.Id }, created);
        }

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
                return NotFound();

            return Ok(bid);
        }


        [Authorize(Policy = "AdminAccess")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vm.Id || vm.Id == 0)
                return BadRequest();

            var updated = await _bidListService.UpdateBidList(vm);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var exists = await _bidListService.GetBidId(id);

            if (exists == null)
                return NotFound();
            
            await _bidListService.DeleteBidList(id);

            return NoContent(); // 204 NoContent
        }


    }
}
