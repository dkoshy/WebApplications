using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient("category",
     client =>
     {
         client.BaseAddress = new Uri("http://localhost:5245/api/");
         client.DefaultRequestHeaders.Accept.Clear();
     })
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    })
    .ConfigurePrimaryHttpMessageHandler(handler =>
    {
        var httpHandler = new SocketsHttpHandler();
        return httpHandler;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
