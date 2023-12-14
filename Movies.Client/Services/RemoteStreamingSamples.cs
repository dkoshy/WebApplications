using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class RemoteStreamingSamples : IIntegrationService
{
    private readonly HttpClient _httpclient;

    public RemoteStreamingSamples(MovieHttpClient movieHttpClient)
    {
        _httpclient = movieHttpClient._httpClient;
    }
    public async Task RunAsync()
    {
        await GetMovieStream();
    }

    public async Task<IEnumerable<Movie>> GetMovieStream()
    {
        var movies =  new List<Movie>();
        var request = new HttpRequestMessage(HttpMethod.Get
                ,"moviesstream");
        
      using(  var response = await _httpclient.SendAsync(request
            , HttpCompletionOption.ResponseHeadersRead))
      {
          response.EnsureSuccessStatusCode();
          var streamcontent = await response.Content.ReadAsStreamAsync();
          var data =  JsonSerializer
            .DeserializeAsyncEnumerable<Movie>(streamcontent
            , new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
          await foreach(var movie in data)
          {
            movies.Add(movie);
          }

          return movies;
         
      }

         
    }


}
