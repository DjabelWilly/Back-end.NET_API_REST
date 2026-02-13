using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("rating")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // -------------------- GET ALL --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ratingService.GetAllRatings();
            return Ok(list);
        }

        // -------------------- GET BY ID --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RatingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _ratingService.GetRatingById(id);
            if (rating == null)
                return NotFound(new { message = $"Rating avec Id={id} non trouvé." });

            return Ok(rating);
        }

        // -------------------- CREATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(RatingViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Données invalides" });

            var created = await _ratingService.SaveRating(vm);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // -------------------- UPDATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RatingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] RatingViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Données invalides" });

            if (vm.Id != id || vm.Id == 0)
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet fourni." });

            var exists = await _ratingService.GetRatingById(id);

            if (exists == null)
                return NotFound(new { message = $"Rating avec Id={id} non trouvé." });

            var updated = await _ratingService.UpdateRating(vm);

            return Ok(updated);
        }


        // -------------------- DELETE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _ratingService.GetRatingById(id);
            if (exists == null)
                return NotFound(new { message = $"Rating avec Id={id} non trouvé." });

            await _ratingService.DeleteRating(id);

            return NoContent();
        }
    }
}