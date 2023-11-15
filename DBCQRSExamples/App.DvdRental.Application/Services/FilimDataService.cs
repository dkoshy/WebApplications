using App.DvdRental.Application.Interfaces;
using App.DvdRental.Data.Repository;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Services
{
    public class FilimDataService : IFilimDataService
    {
        private readonly FilimRepository _filimRepository;

        public FilimDataService(FilimRepository filimRepository)
        {
            _filimRepository= filimRepository as FilimRepository;
        }
        public async Task<IEnumerable<Film>> GetAllFilimsAsync()
        {
            var result =  await _filimRepository.GetAllAsync();
            return result.Data;
        }

        public async Task<Film> GetFilimById(int id)
        {
            var result = await _filimRepository.GetByIdAsync(id);
            return result.Data;
        }
    }
}
