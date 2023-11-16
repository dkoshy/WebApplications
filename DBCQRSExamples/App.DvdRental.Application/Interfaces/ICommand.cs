using MediatR;

namespace App.DvdRental.Application.Interfaces;
public interface ICommand<TResponse>:IRequest<TResponse>
{

}
