using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointService _curvePointService;

        public CurvePointController(ICurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCurvePoints()
        {
            var result = await _curvePointService.GetAllCurvePoints();
            return Ok(result);
        }

        [Authorize(Policy = "UserAccess")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CurvePointViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurvePointById(int id)
        {
            var result = await _curvePointService.GetCurvePointById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [Authorize(Policy = "AdminAccess")]
        [HttpPost]
        [ProducesResponseType(typeof(CurvePointViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _curvePointService.SaveCurvePoint(vm);

            return CreatedAtAction(nameof(GetCurvePointById), new { id = created.Id }, created);
        }

        [Authorize(Policy = "AdminAccess")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vm.Id || vm.Id == 0)
                return BadRequest("Id mismatch");

            // Vérification existence pour éviter les 500
            var updated = await _curvePointService.GetCurvePointById(id);
           
            if (updated == null)
                return NotFound();

            await _curvePointService.UpdateCurvePoint(vm);

            return Ok(updated);
        }

        [Authorize(Policy = "AdminAccess")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _curvePointService.GetCurvePointById(id);
            if (exists == null)
                return NotFound();

            await _curvePointService.DeleteCurvePoint(id);

            return NoContent(); // 204 NoContent

        }
    }
}
