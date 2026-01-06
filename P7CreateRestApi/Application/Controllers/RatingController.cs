using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ratingService.GetAllRatings();
            return Ok(list);
        }

        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _ratingService.GetRatingById(id);
            if (rating == null)
                return NotFound();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RatingViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ratingService.SaveRating(vm);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (vm.Id != id)
                return BadRequest("Id mismatch");

            var existing = await _ratingService.GetRatingById(id);
            if (existing == null)
                return NotFound();

            await _ratingService.UpdateRating(vm);

            return Ok(new { message = "update done successfully" });
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _ratingService.GetRatingById(id);
            if (exists == null)
                return NotFound();

            await _ratingService.DeleteRating(id);
            return Ok(new { message = "delete done successfully" });
        }
    }
}