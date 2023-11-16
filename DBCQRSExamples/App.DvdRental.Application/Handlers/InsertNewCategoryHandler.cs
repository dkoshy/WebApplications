using App.DvdRental.Application.Interfaces;
using App.DvdRental.Application.Models.Commands;
using App.DvdRental.Data.DbContexts;
using App.DvdRental.Domain.Models.Entity;
using MediatR;

namespace App.DvdRental.Application.Handlers;
public class InsertNewCategoryHandler : IRequestHandler<InsertNewCategory, Category>
{
    private readonly ICategoryService _categoryService;

    public InsertNewCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public  async Task<Category> Handle(InsertNewCategory request, CancellationToken cancellationToken)
    {
        return await _categoryService.CreateNewCategory(request.newCat);
    }
}
