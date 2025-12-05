using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
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

        // GET: /curvepoint
        [HttpGet]
        public async Task<IActionResult> GetAllCurvePoints()
        {
            var result = await _curvePointService.GetAllCurvePoints();
            return Ok(result);
        }

        // GET: /curvepoint/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurvePointById(int id)
        {
            var result = await _curvePointService.GetCurvePointById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: /curvepoint
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _curvePointService.SaveCurvePoint(vm);
            return CreatedAtAction(nameof(GetCurvePointById), new { id = created.Id }, created);
        }

        // PUT: /curvepoint/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePointViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vm.Id)
                return BadRequest("L'Id ne correspond pas.");

            await _curvePointService.UpdateCurvePoint(vm);
            return NoContent();
        }

        // DELETE: /curvepoint/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _curvePointService.DeleteCurvePoint(id);
            return NoContent();
        }
    }
}
