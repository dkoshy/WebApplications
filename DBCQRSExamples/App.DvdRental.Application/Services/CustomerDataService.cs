using App.DvdRental.Application.Interfaces;
using App.DvdRental.Data.Repository;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly CustomerRepository _repository;

        public CustomerDataService(CustomerRepository repository)
        {
            _repository=repository;
        }
        public async Task<IEnumerable<Customer>> GetcustomerDetails(int? custId = null)
        {
           var result = await  _repository.GetAllAsync();
            return result.Data;
        }
    }
}
