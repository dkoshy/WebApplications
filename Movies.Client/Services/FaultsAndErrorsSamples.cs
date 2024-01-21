using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class FaultsAndErrorsSamples : IIntegrationService
{
    private readonly HttpClient _httpClient;

    public FaultsAndErrorsSamples(MovieHttpClient client)
    {
        _httpClient = client._httpClient;

    }
    public async Task RunAsync()
    {
        await GetMoviewithError(CancellationToken.None);
       //await CreateMovieWithvalidationError();
    }

    public async Task<Movie?> GetMoviewithError(CancellationToken token)
    {
        var req = new HttpRequestMessage(HttpMethod.Get,
         "movies/030a43b0-f9a5-405a-811c-bf342524b2be");
        var response = await _httpClient.SendAsync(req);

        // try
        // {
        if(!response.IsSuccessStatusCode)
        {
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Not Found");
                return null;
                
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
        }
          
            var content = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<Movie>(content,
             new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return data;
        // }
        // catch (HttpRequestException ex)
        // {
        //     Console.WriteLine("Operation Request Exception");
        //     return null;
        // }


    }

    /// <summary>
    /// Getting validation result
    /// </summary>
    /// <returns></returns>
    public async Task<Movie?> CreateMovieWithvalidationError()
    
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            "movies");
        var newmovie = new MovieForCreation(){ };
        var memeorystream = new MemoryStream();
        await JsonSerializer.SerializeAsync(memeorystream,newmovie);

        using var content = new StreamContent(memeorystream);
        content.Headers.ContentType=new MediaTypeWithQualityHeaderValue("application/json");
        request.Content=content;

        using var response = await _httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead);
        if(!response.IsSuccessStatusCode)
        {
            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                // var errorStream = await response.Content.ReadAsStreamAsync();
                // var problemDetails = await JsonSerializer.DeserializeAsync<ProblemDetails>(errorStream);
                Console.WriteLine("Bad Request");
                return null;
            }

            response.EnsureSuccessStatusCode();
        }

        var responseContent = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<Movie>(responseContent);
        
        return data;
    }


    public async Task<int?> GetInventoryDetails()
    {
        
    }
}