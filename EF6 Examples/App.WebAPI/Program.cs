using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherData.Repo;
using PublisherDomain;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<PublisherDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("pubDbConnection"))
           .EnableSensitiveDataLogging()
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();



app.MapGet("api/authors", async Task<Results<NotFound,Ok<IEnumerable<AuthorDto>>>> (AuthorRepository _aurepo) =>
{
    var result = await _aurepo.GetAuthorsAsync();
    if (result == null)
        return TypedResults.NotFound(); 

    return TypedResults.Ok(result);
});


app.MapGet("api/authors/{id}", async Task<Results<NotFound, Ok<AuthorDto>>> (AuthorRepository _aurepo,
                int id) =>
{
    var result = await _aurepo.FindAsync(id);
    if (result == null)
        return TypedResults.NotFound();

    return TypedResults.Ok(result);
});

app.MapGet("api/authors/{id}/books", async Task<Results<NotFound,
        Ok<IEnumerable<BookAuthorDto>>>> (BookRepository _bookRepo,
        int id) =>
{
    var result = await _bookRepo.GetAllBooksByAuthor(id);
    if (result == null)
        return TypedResults.NotFound();

    return TypedResults.Ok(result);
});

app.MapGet("api/books", async Task<Results<NotFound,
        Ok<IEnumerable<BookDto>>>> (BookRepository _bookRepo
        ) =>
{
    var result = await _bookRepo.GetAllAsync();
    if (result == null)
        return TypedResults.NotFound();

    return TypedResults.Ok(result);
});

app.MapGet("api/books/{id}", async Task<Results<NotFound
    ,Ok<BookCoverDto>>> (BookRepository _bookRepo,
    int id) =>
{ 
  var result = await _bookRepo.GetBookWithCoverDetailsAsync(id);
    if (result == null)
        return TypedResults.NotFound();

    return TypedResults.Ok(result);
    
});

app.Run();


