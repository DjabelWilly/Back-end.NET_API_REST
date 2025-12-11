using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BidListViewModel vm)
        {
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(int id)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBid(int id, [FromBody] BidListViewModel vm)
        {

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            return NoContent();
        }

    }
}