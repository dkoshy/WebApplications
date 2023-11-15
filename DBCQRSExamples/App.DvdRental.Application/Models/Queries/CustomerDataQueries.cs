using App.DvdRental.Application.Interfaces;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Models.Queries
{
    public record  GetCustomers(int? CustId):IQuery<IEnumerable<Customer>>;
    
}
