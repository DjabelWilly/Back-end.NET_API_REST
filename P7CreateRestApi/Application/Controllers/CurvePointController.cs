using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("curvepoint")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurvePointController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        // -------------------- GET ALL --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCurvePoints()
        {
            var result = await _curvePointService.GetAllCurvePoints();
            return Ok(result);
        }

        // -------------------- GET BY ID --------------------
        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CurvePointViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurvePointById(int id)
        {
            var result = await _curvePointService.GetCurvePointById(id);

            if (result == null)
                return NotFound(new { message = $"CurvePoint avec Id={id} non trouvée." });

            return Ok(result);
        }

        // -------------------- CREATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(CurvePointViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Données invalides" });

            var created = await _curvePointService.SaveCurvePoint(vm);

            return CreatedAtAction(nameof(GetCurvePointById), new { id = created.Id }, created);
        }

        // -------------------- UPDATE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Données invalides" });

            if (id != vm.Id || vm.Id == 0)
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet fourni." });

            // Vérification existence pour éviter les 500
            var exists = await _curvePointService.GetCurvePointById(id);

            if (exists == null)
                return NotFound(new { message = $"CurvePoint avec Id={id} non trouvée." });

            var updated = await _curvePointService.UpdateCurvePoint(vm);

            return Ok(updated);
        }

        // -------------------- DELETE --------------------
        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _curvePointService.GetCurvePointById(id);
            if (exists == null)
                return NotFound(new { message = $"CurvePoint avec Id={id} non trouvée." });

            await _curvePointService.DeleteCurvePoint(id);

            return NoContent();

        }
    }
}
