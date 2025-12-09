using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
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


        [HttpPost]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _bidListService.SaveBidList(vm);

            return CreatedAtAction(nameof(GetBidById), new { id = created.Id }, created);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BidListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBidById(int id)
        {
            var result = await _bidListService.GetBidId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vm.Id)
                return BadRequest("Id mismatch.");

            // Vérification existence pour éviter les 500 EF Core
            var existing = await _bidListService.GetBidId(id);
            if (existing == null)
                return NotFound();

            await _bidListService.UpdateBidList(vm);

            return Ok(new { message = "update done successfully" });
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var exists = await _bidListService.GetBidId(id);
            if (exists == null)
                return NotFound();

            await _bidListService.DeleteBidList(id);

            return Ok(new { message = "delete done successfully" });
        }

    }
}