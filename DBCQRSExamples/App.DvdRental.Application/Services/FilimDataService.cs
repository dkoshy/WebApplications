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
            _filimRepository=filimRepository;
        }
        public async Task<IEnumerable<Filim>> GetAllFilimsAsync()
        {
            var result =  await _filimRepository.GetAllAsync();
            return result.Data;
        }
    }
}
