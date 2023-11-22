using System.Net.Http.Headers;
using System.Text.Json;

namespace Movies.Client.Helpers;
public static class MovieClientExtentions
{
  
  public static string  ToJsonData<T>(this T result) where T:class
  {
     return JsonSerializer.Serialize(result 
        , new JsonSerializerOptions(JsonSerializerDefaults.Web) );
  }

  public static  T?  ToObject<T>(this string content) where T:class
  {
     return JsonSerializer.Deserialize<T>(content
      ,new JsonSerializerOptions(JsonSerializerDefaults.Web));
  }

  public static HttpRequestMessage GetRequestMessage( string resourceUri)
  {
      var request = new HttpRequestMessage(HttpMethod.Get
         ,resourceUri);
      request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      return request;
  }

}
