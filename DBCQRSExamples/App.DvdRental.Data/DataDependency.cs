using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Repository;
using App.DvdRental.Domain.Interfaces;
using App.DvdRental.Domain.Models.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace App.DvdRental.Data
{
    public static class DataDependency
    {

        public static IServiceCollection AddDataServices(this IServiceCollection service )
        {
            service.AddSingleton<DapperContext>();
            service.AddScoped<IRepository<Filim> , FilimRepository>();

            return service;
        }
    }
}
