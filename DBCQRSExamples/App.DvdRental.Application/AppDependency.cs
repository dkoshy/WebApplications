using App.DvdRental.Application.Interfaces;
using App.DvdRental.Application.Services;
using App.DvdRental.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.DvdRental.Application
{
    public static class AppDependency
    {

        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            //Add Add MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AppDependency).Assembly);
            });
            //Add data services
            services.AddDataServices(config);
            //Add app services
            services.AddScoped<IFilimDataService, FilimDataService>();
            services.AddScoped<ICustomerDataService, CustomerDataService>();
            services.AddScoped<ICategoryService,CategoryService>();
            return services;
        }
    }
}
