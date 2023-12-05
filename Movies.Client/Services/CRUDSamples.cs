using System.Net.Http.Headers;
using Movies.Client.Helpers;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class CRUDSamples : IIntegrationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _movieHttpclient;
    private readonly MovieHttpClient _clientForMovieHttp;

    public CRUDSamples(IHttpClientFactory httpClientFactory
    ,MovieHttpClient movieHttp)
    {
        _httpClientFactory = httpClientFactory;
        _movieHttpclient = movieHttp._httpClient;
        _clientForMovieHttp = movieHttp;
    }
    public async Task RunAsync()
    {
       // await GetallMoies();
       //await GetMovieByNamedClient();
       //await GetMoviesByTypedClient();
       //await GetMoviesByTypedClientMethod();
       //await GetMoviesById();
       //await CreateMovie();
       //await Delete();
       await Update();
    }

    public  async Task<IEnumerable<Movie>?> GetallMoies()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5001/api/");

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json") );
        
        var response = await client.GetAsync("movies");

        var result = await response.Content.ReadAsStringAsync();
        
        var data =  result.ToObject<IEnumerable<Movie>>();
        return data;
    }
    public async Task<IEnumerable<Movie>?> GetMovieByNamedClient()
    {
        var client = _httpClientFactory.CreateClient("movieClient");
        var request = new HttpRequestMessage(
            HttpMethod.Get
            ,"movies"
        );
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var data = content.ToObject<IEnumerable<Movie>>();
        return data;
    }

    public async Task<IEnumerable<Movie>?> GetMoviesByTypedClient()
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get
            ,"movies"
             );
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await _movieHttpclient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var data = content.ToObject<IEnumerable<Movie>>();
        return data;

    }

    public async Task<IEnumerable<Movie>?> GetMoviesByTypedClientMethod()
    {
        var movies = await _clientForMovieHttp.GetMovies();
        return movies;
    }

    public async Task<Movie?> GetMoviesById()
    {
        var movie = await _clientForMovieHttp.GetMovieById(new Guid("26fcbcc4-b7f7-47fc-9382-740c12246b59"));
        return movie;
    } 

    public async Task<Movie> CreateMovie()
    {
        var movie = await _clientForMovieHttp.CrateaNewMovie();
        return movie;
    }

    public async Task Delete()
    {
        await _clientForMovieHttp.DeleteMovies(new Guid("3fcf8012-60e2-4d08-b4db-404e79c06918"));
    }

    public async Task Update()
    {
        var updatedData = await _clientForMovieHttp.UpdateMovie(new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"));
    }
    
} 
