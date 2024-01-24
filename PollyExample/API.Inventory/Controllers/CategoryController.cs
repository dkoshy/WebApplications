using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Inventory.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("category");

            var request = new HttpRequestMessage(HttpMethod.Get, $"inventory/{id}");

            var response = await  client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);

        }
    }
}
