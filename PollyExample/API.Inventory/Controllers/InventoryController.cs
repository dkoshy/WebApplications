using Microsoft.AspNetCore.Mvc;

namespace API.Inventory.Controllers
{
    
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController: ControllerBase
    {
        public InventoryController()
        {
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory(int id)
        {
            return Ok(21);
        }

    }
}
