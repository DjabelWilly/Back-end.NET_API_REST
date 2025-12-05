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


        // Créer un BidList
        [HttpPost ("create")]
        public async Task<IActionResult> Create([FromBody] BidListViewModel vm)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // renvoie les erreurs de validation

            // Appel du service l'entité via le service
            var entityCreated = await _bidListService.SaveBidList(vm);

            // Retourne l'objet sauvegardé avec un code HTTP 200 OK
            return Ok(entityCreated);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(int id)
        {
            var result = await _bidListService.GetBidId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Vérifie les champs 'required', si valid -> appelle le service pour update et return BidList
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vm.Id)
                return BadRequest("L'Id ne correspond pas.");

            await _bidListService.UpdateBidList(vm);

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            await _bidListService.DeleteBidList(id);
            return NoContent();
        }

    }
}