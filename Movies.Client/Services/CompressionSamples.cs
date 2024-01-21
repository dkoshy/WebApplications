using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;

namespace Movies.Client.Services;

public class CompressionSamples : IIntegrationService
{
    private readonly HttpClient _httpClient;

    public CompressionSamples(MovieHttpClient movieHttpClient)
    {
        _httpClient = movieHttpClient._httpClient;
    }
    public async Task RunAsync()
    {
        //await GetMovieTrailer();
        await CreateNewTrailereAsync();
    }


    public async Task<Trailer> GetMovieTrailer()
    {
        var movieId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");
        var request = new HttpRequestMessage(HttpMethod.Get
                , $"movies/{movieId}/trailers/{movieId}");
        request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        using (var response = await _httpClient.SendAsync(request
            , HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            var trailer = await JsonSerializer.DeserializeAsync<Trailer>(content
            , new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return trailer;
        }

    }

    public async Task<Trailer> CreateNewTrailereAsync()
    {
        var movieId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");

        var random = new Random();
        //var genrateByteLength = random.Next(52428800, 104857600);
        var generatedBytes = new byte[2500900];
        random.NextBytes(generatedBytes);

        var trailer = new TrailerForCreation()
        {
            MovieId = movieId,
            Name="some trailer",
            Description = $"New trailere for {movieId}",
            Bytes = generatedBytes
        };

        var memmorystream = new MemoryStream();
        await JsonSerializer.SerializeAsync(memmorystream
            , trailer);
        memmorystream.Seek(0, SeekOrigin.Begin);

        using (var request = new HttpRequestMessage(HttpMethod.Post
                , $"movies/{movieId}/trailers"))
        {
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            var compressedstream = new MemoryStream();
            var gzipstream = new GZipStream(compressedstream,
            CompressionMode.Compress);

            memmorystream.CopyTo(gzipstream);
            gzipstream.Flush();
            compressedstream.Position = 0;

            using (var actualstream = new StreamContent(compressedstream))
            {
                actualstream.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                actualstream.Headers.ContentEncoding.Add("gzip");
                request.Content = actualstream;
                var response = await _httpClient.SendAsync(request
                     , HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();
                var newTrailer = await JsonSerializer.DeserializeAsync<Trailer>(content
                , new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return newTrailer;

            }

        }

    }
}

