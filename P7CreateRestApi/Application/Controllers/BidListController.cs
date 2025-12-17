using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBidById(int id)
        {
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id)
        {
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            return Ok();
        }

    }
}