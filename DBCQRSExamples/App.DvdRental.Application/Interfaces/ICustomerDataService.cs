using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Interfaces
{
    public interface ICustomerDataService
    {
        Task<IEnumerable<Customer>> GetcustomerDetails(int? custId = null);
    }
}
