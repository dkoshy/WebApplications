using System.Net.Http.Headers;
using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class LocalStreamsSamples : IIntegrationService
{
    private readonly HttpClient _httpclient;

    public LocalStreamsSamples(MovieHttpClient movieHttpClient)
    {
        _httpclient = movieHttpClient._httpClient;
    }
    public async Task RunAsync()
    {
        //await GetPosterTaskAsync();
        //await AddPosterForMovieWitoutStreamContent();
        await AddPosterForMovie();
    }

    public async Task<Poster> GetPosterTaskAsync()
    {
        Guid movieId, posterId;
        movieId = posterId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");

        var request = new HttpRequestMessage(HttpMethod.Get
                , $"movies/{movieId}/posters/{posterId}");
        using (var response = await _httpclient.SendAsync(request
        , HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            var contentSteram = await response.Content.ReadAsStreamAsync();
            var poster = await JsonSerializer.DeserializeAsync<Poster>(contentSteram
            , new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return poster;
        }
    }

    public async Task<Poster> AddPosterForMovieWitoutStreamContent()
    {
        var movieId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");

        // generate a movie poster of 5MB
        var random = new Random();
        var generatedBytes = new byte[5242880];
        random.NextBytes(generatedBytes);

        var newPoster = new PosterForCreation
        {
            Name = "poster Newly created",
            Bytes = generatedBytes
        };

        var jsondata = JsonSerializer.Serialize(newPoster
        , new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var request = new HttpRequestMessage(HttpMethod.Post
               , $"movies/{movieId}/posters");
        request.Content = new StringContent(jsondata);
        request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

        using (var response = await _httpclient.SendAsync(request
                 , HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            var poster = JsonSerializer.Deserialize<Poster>(content
                        , new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return poster;
        }
    }


    public async Task<Poster> AddPosterForMovie()
    {
        var movieId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");

        // generate a movie poster of 5MB
        var random = new Random();
        var generatedBytes = new byte[5242880];
        random.NextBytes(generatedBytes);

        var newPoster = new PosterForCreation
        {
            Name = "poster Newly created",
            Bytes = generatedBytes
        };

        using var memoryStreamContent = new MemoryStream();

        JsonSerializer.Serialize(memoryStreamContent, newPoster
        , new JsonSerializerOptions(JsonSerializerDefaults.Web));
        memoryStreamContent.Seek(0, SeekOrigin.Begin);

        using (var request = new HttpRequestMessage(HttpMethod.Post
               , $"movies/{movieId}/posters"))
        {
            using var streamContent = new StreamContent(memoryStreamContent);
            request.Content = streamContent;
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = await _httpclient.SendAsync(request
                , HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var content = await response.Content.ReadAsStreamAsync();
            var poster = JsonSerializer.Deserialize<Poster>(content
                        , new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return poster;
        }
    }
}
