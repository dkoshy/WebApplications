using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Movies.Client.Services;
using Movies.Client.Services.DataClients;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    { 
        // register services for DI
        services.AddLogging(configure => configure.AddDebug().AddConsole());
        services.AddHttpClient(); //default injecting

        services.AddHttpClient("movieClient", config =>
        {
            config.BaseAddress = new Uri("http://localhost:5001/api/");
            config.DefaultRequestHeaders.Clear();

        })
        .ConfigurePrimaryHttpMessageHandler(handler => //can't be override.
        {
            var httphandler = new SocketsHttpHandler();
            httphandler.AllowAutoRedirect = false;
            return httphandler;
        })
        .ConfigureHttpClient(options =>
        {
            options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });


        services.AddHttpClient<MovieHttpClient>()
        .ConfigureHttpClient(options => {
            options.BaseAddress = new Uri("http://localhost:5001/api/");
            options.Timeout = new TimeSpan(0,0,30);
            options.DefaultRequestHeaders.Accept.Clear();
            options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           })
        .ConfigurePrimaryHttpMessageHandler(config =>{
            var handler = new SocketsHttpHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip;
            return  handler;
        });
        

        // For the cancellation samples
        // services.AddScoped<IIntegrationService, CancellationSamples>();

        // For the compression samples
        // services.AddScoped<IIntegrationService, CompressionSamples>();

        // For the CRUD samples
        //services.AddScoped<IIntegrationService, CRUDSamples>();

        // For the compression samples
        // services.AddScoped<IIntegrationService, CompressionSamples>();

        // For the custom message handler samples
        // services.AddScoped<IIntegrationService, CustomMessageHandlersSamples>();

        // For the faults and errors samples
        // services.AddScoped<IIntegrationService, FaultsAndErrorsSamples>();

        // For the HttpClientFactory samples
        //services.AddScoped<IIntegrationService, HttpClientFactorySamples>();

        // For the local streams samples
       //  services.AddScoped<IIntegrationService, LocalStreamsSamples>();

        // For the partial update samples
        // services.AddScoped<IIntegrationService, PartialUpdateSamples>();

        // For the remote streaming samples
       //services.AddScoped<IIntegrationService, RemoteStreamingSamples>();

    }).Build();



// For demo purposes: overall catch-all to log any exception that might 
// happen to the console & wait for key input afterwards so we can easily 
// inspect the issue.  
try
{
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Host created.");

    // Run the IntegrationService containing all samples and
    // await this call to ensure the application doesn't 
    // prematurely exit.
    await host.Services.GetRequiredService<IIntegrationService>().RunAsync();
}
catch (Exception generalException)
{
    // log the exception
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(generalException,
        "An exception happened while running the integration service.");
}

Console.Read();

await host.RunAsync();
 
 