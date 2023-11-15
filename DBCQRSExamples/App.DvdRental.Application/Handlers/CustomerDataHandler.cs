using App.DvdRental.Application.Interfaces;
using App.DvdRental.Application.Models.Queries;
using App.DvdRental.Application.Services;
using App.DvdRental.Domain.Models.Entity;

namespace App.DvdRental.Application.Handlers
{
    public class GetCustomersHandler : IQueryHandler<GetCustomers, IEnumerable<Customer>>
    {
        private readonly ICustomerDataService _service;

        public GetCustomersHandler(ICustomerDataService service)
        {
            _service=service;
        }
        public async Task<IEnumerable<Customer>> Handle(GetCustomers request, CancellationToken cancellationToken)
        {
            return await _service.GetcustomerDetails();
        }
    }
}
