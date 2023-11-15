using App.DvdRental.Application.Interfaces;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Models.Queries
{
   public record GetAllFilimData():IQuery<IEnumerable<Film>>;
   public record GetFilimDataById(int id):IQuery<Film>;
}
