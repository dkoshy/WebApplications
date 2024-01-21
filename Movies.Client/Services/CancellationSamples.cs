using System.Net.Http.Headers;
using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class CancellationSamples : IIntegrationService
{
    private readonly HttpClient _client;
    private readonly CancellationTokenSource _tokenSource =
        new CancellationTokenSource();

    public CancellationSamples(MovieHttpClient movieHttpClient)
    {
        _client = movieHttpClient._httpClient;

    }

    public async Task RunAsync()
    {
        _tokenSource.CancelAfter(200);
        await GetMovieTrailer(_tokenSource.Token);
    }


    public async Task<Trailer?> GetMovieTrailer(CancellationToken token)
    {
        var movieId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");
        var request = new HttpRequestMessage(HttpMethod.Get,
               $"movies/{movieId}/trailers/{movieId}");
        request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        try
        {
            using (var response = await _client.SendAsync(request
        , HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Trailer>(content,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return data;
            }
        }
        catch (OperationCanceledException opex)
        {
            Console.WriteLine($"Opration canceled exception.");
            return default;
        }
    }
}
