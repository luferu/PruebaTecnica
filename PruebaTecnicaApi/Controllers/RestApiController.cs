using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.Shared;

namespace PruebaTecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestApiController : ControllerBase
    {
        private readonly IRestApiService _restApiService;
        public RestApiController(IRestApiService restApiService)
        {
            _restApiService = restApiService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _restApiService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        // public async Task<IActionResult> GetByIdAsync(string id) => Ok(await _restApiService.GetByIdAsync(id));

        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _restApiService.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] RestApiDTO obj) => Ok(await _restApiService.CreateAsync(obj));

        //public async Task<IActionResult> CreateAsync([FromBody] RestApiDTO obj)
        //{
        //    var result = await _restApiService.CreateAsync(obj);
        //    return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        //}


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] RestApiDTO obj) => Ok(await _restApiService.UpdateAsync(id, obj));

       
    }
}

