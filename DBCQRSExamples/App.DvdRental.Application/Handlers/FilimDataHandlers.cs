using App.DvdRental.Application.Interfaces;
using App.DvdRental.Application.Models.Queries;
using App.DvdRental.Domain.Models.Entity;
using MediatR;

namespace App.DvdRental.Application.Handlers
{

    public class GetAllFilimDataHandler : IQueryHandler<GetAllFilimData, IEnumerable<Film>>
    {
        private readonly IFilimDataService _dataService;

        public GetAllFilimDataHandler(IFilimDataService dataService)
        {
            _dataService=dataService;
        }
        public async Task<IEnumerable<Film>> Handle(GetAllFilimData request, CancellationToken cancellationToken)
        {
            return await _dataService.GetAllFilimsAsync();
        }
    }

    public class GetFilimDataByIdHandler : IQueryHandler<GetFilimDataById, Film>
    {

        private readonly IFilimDataService _dataService;

        public GetFilimDataByIdHandler(IFilimDataService dataService)
        {
            _dataService=dataService;
        }
        public async Task<Film> Handle(GetFilimDataById request, CancellationToken cancellationToken)
        {
           return await _dataService.GetFilimById(request.id);
        }
    }


}


