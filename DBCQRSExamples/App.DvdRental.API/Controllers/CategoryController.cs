using App.DvdRental.API.Extention;
using App.DvdRental.API.Models;
using App.DvdRental.Application.Models.Commands;
using App.DvdRental.Domain.Models.Entity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.DvdRental.API.Controllers;

[Route("api/dvdrental/")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CategoryController(ILogger<CategoryController> logger
            , ISender sender
            , IMapper mapper)
    { 
        _logger = logger;
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("category")]
    public async Task<ActionResult<APIResponseBase>> Post([FromBody] CategoryDto newCat)
    {
        var category = _mapper.Map<Category>(newCat);
        var data = await _sender.Send(new InsertNewCategory(category));
        return data.AsResponse();
    }

}
