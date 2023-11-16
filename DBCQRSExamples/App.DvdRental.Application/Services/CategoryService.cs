using App.DvdRental.Application.Interfaces;
using App.DvdRental.Data.DbContexts;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly DvdRentalEFContext _ctx;

    public CategoryService(DvdRentalEFContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<Category> CreateNewCategory(Category newCat)
    {
        _ctx.Categories.Add(newCat);
        await _ctx.SaveChangesAsync();
        return newCat;
    }
}
