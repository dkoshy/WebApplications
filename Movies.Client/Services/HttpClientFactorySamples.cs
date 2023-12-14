using System.Net.Http;
using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class HttpClientFactorySamples : IIntegrationService
{
    private readonly HttpClient _movieHttpClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientFactorySamples(MovieHttpClient movieHttpClient
            , IHttpClientFactory httpClientFactory)
    {
        _movieHttpClient = movieHttpClient._httpClient;
        _httpClientFactory = httpClientFactory;
    }
    public async Task RunAsync()
    {
       // await GetFilims();
        await GetFilimsRedirection();
    }

    /// <summary>
    /// This should throw exception since named client
    /// desnot support redirection.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Movie>> GetFilims()
    {
        var client = _httpClientFactory.CreateClient("movieClient");
        var reuest = new HttpRequestMessage(HttpMethod.Get
          ,"films");

        var response = await client.SendAsync(reuest);
        response.EnsureSuccessStatusCode();
        var content =  await response.Content.ReadAsStringAsync();
        var data =  JsonSerializer.Deserialize<IEnumerable<Movie>>(content
                        ,new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;
    }

    public async Task<IEnumerable<Movie>> GetFilimsRedirection()
    {
        var reuest = new HttpRequestMessage(HttpMethod.Get
          ,"films");

        var response = await _movieHttpClient.SendAsync(reuest);
        response.EnsureSuccessStatusCode();
        var content =  await response.Content.ReadAsStringAsync();
        var data =  JsonSerializer.Deserialize<IEnumerable<Movie>>(content
                        ,new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;
    }

}
