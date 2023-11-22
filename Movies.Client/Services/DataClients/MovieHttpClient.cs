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
        var response =  await _httpClient.SendAsync(getRequest);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var data = content.ToObject<IEnumerable<Movie>>();
        return data;
    }
}
