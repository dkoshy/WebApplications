using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Interfaces;
public interface  ICategoryService
{
    Task<Category> CreateNewCategory(Category newCat);
}
