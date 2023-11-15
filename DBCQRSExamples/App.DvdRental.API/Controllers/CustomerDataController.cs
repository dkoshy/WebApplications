using App.DvdRental.API.Extention;
using App.DvdRental.API.Models;
using App.DvdRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.DvdRental.API.Controllers
{
    [Route("api/dvdrental/")]
    [ApiController]
    public class CustomerDataController : ControllerBase
    {
        private readonly ILogger<CustomerDataController> _logger;
        private readonly ICustomerDataService _dataService;

        public CustomerDataController(ILogger<CustomerDataController> logger
            , ICustomerDataService dataService)
        {
            _logger=logger;
            _dataService=dataService;
        }

        [HttpGet("customers")]
        public async Task<ActionResult<APIResponseBase>> CustomerDetails()
        {
            var data = await  _dataService.GetcustomerDetails(null);
            return data.AsResponse();
        }
    }
}
