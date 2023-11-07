using App.DvdRental.API.Extention;
using App.DvdRental.API.Models;
using App.DvdRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.DvdRental.API.Controllers
{
    [ApiController]
    [Route("api/dvdrental/")]
    public class FilimDataController : ControllerBase
    {
       
        private readonly ILogger<FilimDataController> _logger;
        private readonly IFilimDataService _filimDataService;

        public FilimDataController(ILogger<FilimDataController> logger
            , IFilimDataService filimDataService)
        {
            _logger = logger;
            _filimDataService=filimDataService;
        }

        [HttpGet("filims")]
        public async Task<ActionResult<APIResponseBase>> Get()
        {
            var data =   await _filimDataService.GetAllFilimsAsync();
            return data.AsResponse();
        }
    }
}