using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Interfaces
{
    public interface IFilimDataService
    {
        Task<IEnumerable<Filim>> GetAllFilimsAsync();
    }
}
