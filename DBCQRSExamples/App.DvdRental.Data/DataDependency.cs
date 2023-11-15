using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace App.DvdRental.Data
{
    public static class DataDependency
    {

        public static IServiceCollection AddDataServices(this IServiceCollection service )
        {
            service.AddSingleton<DapperContext>();
            service.AddScoped<FilimRepository>();
            service.AddScoped<CustomerRepository>();

            return service;
        }
    }
}
