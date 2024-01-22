using Microsoft.AspNetCore.Mvc;

namespace API.Inventory.Controllers;

[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IHttpClientFactory clientFactory;

    public CatalogController(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatalog(int id)
    {
        var httpclient =  clientFactory.CreateClient("category");
        var response = await httpclient.GetAsync($"inventory/{id}");

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();

        return Ok(data);

    }  
}
