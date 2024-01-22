
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace API.Inventory.Controllers;


[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    static int requestCount = 0;
    public InventoryController()
    {

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInventory(int id)
    {
        await Task.Delay(100);
        requestCount++;
        if (requestCount % 4 == 0)
            return Ok(15);

        return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong");
    }

}
