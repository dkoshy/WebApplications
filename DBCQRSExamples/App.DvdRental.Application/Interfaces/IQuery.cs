using MediatR;

namespace App.DvdRental.Application.Interfaces
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
