using App.DvdRental.Data.DbContexts;
using App.DvdRental.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.DvdRental.Data
{
    public static class DataDependency
    {

        public static IServiceCollection AddDataServices(this IServiceCollection service , IConfiguration config)
        {
            service.AddSingleton<DapperContext>();
            service.AddDbContextFactory<DvdRentalEFContext>(options=>{
                options.UseNpgsql(config.GetConnectionString("DVDRentalConnection"));
            });
            service.AddScoped<FilimRepository>();
            service.AddScoped<CustomerRepository>();

            return service;
        }
    }
}
