using App.DvdRental.API.Extention;
using App.DvdRental.API.Models;
using App.DvdRental.Application.Handlers;
using App.DvdRental.Application.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.DvdRental.API.Controllers
{
    [ApiController]
    [Route("api/dvdrental/")]
    public class FilimDataController : ControllerBase
    {
       
        private readonly ILogger<FilimDataController> _logger;
        private readonly ISender _sender;

        public FilimDataController(ILogger<FilimDataController> logger
            , ISender sender)
        {
            _logger = logger;
            _sender=sender;
        }

        [HttpGet("filims")]
        public async Task<ActionResult<APIResponseBase>> Get()
        {
            var data =   await _sender.Send(new GetAllFilimData());
            return data.AsResponse();
        }


        [HttpGet("filims/{id}")]
        public async Task<ActionResult<APIResponseBase>> Get(int id)
        {
            var data = await _sender.Send(new GetFilimDataById(id));
            return data.AsResponse();
        }
    }
}