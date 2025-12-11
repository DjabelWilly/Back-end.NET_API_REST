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