using System.Net.Http.Headers;
using System.Text.Json;
using Movies.Client.Helpers;
using Movies.Client.Models;

namespace Movies.Client.Services.DataClients;
public class MovieHttpClient
{
    public readonly HttpClient _httpClient;

    public MovieHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Movie>?> GetMovies()
    {
        var getRequest = MovieClientExtentions.GetRequestMessage("movies");
        var response = await _httpClient.SendAsync(getRequest);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var data = content.ToObject<IEnumerable<Movie>>();
        return data;
    }

    public async Task<Movie> GetMovieById(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
                $"movies/{id}");
        //request.Content.Headers.ContentType= new MediaTypeWithQualityHeaderValue("application/json");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var data = content.ToObject<Movie>();
        return data;

    }

    public async Task<Movie> CrateaNewMovie()
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
                "movies");
        var jsoData = JsonSerializer.Serialize(new MovieForCreation()
        {
            Title = "Reservoir Dogs",
            Description = "After a simple jewelry heist goes terribly wrong, the " +
                 "surviving criminals begin to suspect that one of them is a police informant.",
            DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
            ReleaseDate = new DateTime(1992, 9, 2),
            Genre = "Crime, Drama"
        });
        request.Content = new StringContent(jsoData);
        request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        /*var data = JsonSerializer.Deserialize<Movie>(content
            , new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });*/

        var data = JsonSerializer.Deserialize<Movie>(content
        , new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;

    }

    public async Task DeleteMovies(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete
        , $"movies/{id}");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Movie?> UpdateMovie(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Put
            , $"movies/{id}");

        request.Content = new StringContent(
            JsonSerializer.Serialize(new MovieForUpdate()
            {
                Title = "Pulp Fiction",
                Description = "The movie with Zed.",
                DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                ReleaseDate = new DateTime(1992, 9, 2),
                Genre = "Crime, Drama"
            })
        );
        request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<Movie>(content,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;

    }

}
