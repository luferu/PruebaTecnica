using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BLL.Interfaces;

namespace PruebaTecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomUserController : ControllerBase
    {
        private readonly IRandomUserService _randomUserService;
        public RandomUserController(IRandomUserService randomUserService)
        {
            _randomUserService = randomUserService;
        }
        [HttpGet("GetAndSendRandomUser")]
        public async Task<IActionResult> GetAndSendRandomUserAsync()
        {
            var result = await _randomUserService.GetRandomUserAndSendWebhookAsync();
            return Ok(new { Success = result });
        }
    }
}
