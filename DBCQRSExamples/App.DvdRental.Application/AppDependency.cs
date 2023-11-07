using App.DvdRental.Application.Interfaces;
using App.DvdRental.Application.Services;
using App.DvdRental.Data;
using Microsoft.Extensions.DependencyInjection;

namespace App.DvdRental.Application
{
    public static class AppDependency
    {

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFilimDataService, FilimDataService>();
            services.AddDataServices();
            return services;
        }
    }
}
