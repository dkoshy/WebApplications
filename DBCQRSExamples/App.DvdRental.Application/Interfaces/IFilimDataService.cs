using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Interfaces
{
    public interface IFilimDataService
    {
        Task<IEnumerable<Film>> GetAllFilimsAsync();
        Task<Film> GetFilimById(int id);
    }
}
