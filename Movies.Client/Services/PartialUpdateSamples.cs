using System.Net.Http.Headers;
using Microsoft.AspNetCore.JsonPatch;
using Movies.Client.Models;
using Movies.Client.Services.DataClients;
using Newtonsoft.Json;

namespace Movies.Client.Services;

public class PartialUpdateSamples : IIntegrationService
{
    private readonly HttpClient _httpclient;

    public PartialUpdateSamples(MovieHttpClient movieHttpclient)
    {
        _httpclient = movieHttpclient._httpClient;
    }

    public async Task RunAsync()
    {
        await PartialUpdateOfMovie(new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"));

    }

    private async Task<Movie> PartialUpdateOfMovie(Guid id)
    {
               
        var patchDoc = new JsonPatchDocument<MovieForUpdate>();
        patchDoc.Replace(m => m.Title, "Updated title");
        patchDoc.Remove(m => m.Description);

        var serializedChangeSet = JsonConvert.SerializeObject(patchDoc);
        var request = new HttpRequestMessage(
            HttpMethod.Patch,
            $"movies/{id}");
       
        request.Content = new StringContent(serializedChangeSet);
        request.Content.Headers.ContentType =
            new MediaTypeHeaderValue("application/json-patch+json");

        var response = await _httpclient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
        return updatedMovie;
    }
}
