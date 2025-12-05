using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ratingService.GetAllRatings();
            return Ok(list);
        }

        // GET /rating/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _ratingService.GetRatingById(id);
            if (rating == null)
                return NotFound();

            return Ok(rating);
        }

        // POST /rating
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ratingService.SaveRating(vm);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /rating/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (vm.Id != id)
                return BadRequest("Id mismatch");

            await _ratingService.UpdateRating(vm);
            return NoContent();
        }

        // DELETE /rating/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ratingService.DeleteRating(id);
            return NoContent();
        }
    }
}
