using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers;

[Route("api/inventory")]
[ApiController]
public class InventoryController : ControllerBase
{
    static int _requestCount = 0;
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInventory(int id)
    {
        // simulate some data processing by delaying for 100 milliseconds 
        await Task.Delay(100);
        _requestCount++;
        if (_requestCount % 4 == 0) // only one of out four requests will succeed
        {
            return Ok(15);
        }

        return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong");
    }


}
