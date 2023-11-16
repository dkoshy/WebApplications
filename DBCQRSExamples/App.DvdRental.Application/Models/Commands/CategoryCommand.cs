using App.DvdRental.Application.Interfaces;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Models.Commands;

public record InsertNewCategory(Category newCat):ICommand<Category>;