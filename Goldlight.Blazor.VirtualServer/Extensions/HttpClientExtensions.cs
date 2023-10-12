using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public static class HttpClientExtensions
{
  public static async Task<T?> Get<T>(this HttpClient client, string uri) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Get, uri));

  public static async Task<T?> Post<T>(this HttpClient client, string uri, T requestBody) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Post, uri, requestBody));

  public static async Task<T?> Put<T>(this HttpClient client, string uri, T requestBody) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Put, uri, requestBody));

  public static async Task Delete<T>(this HttpClient client, string uri) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Delete, uri));

  private static async Task<T?> GetResponseFromServer<T>(HttpClient client, HttpRequestMessage requestMessage)
  {
    HttpResponseMessage response = await client.SendAsync(requestMessage);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<T>();
  }

  private static HttpRequestMessage GetRequestMessage(HttpMethod method, string uri)
  {
    HttpRequestMessage requestMessage = new HttpRequestMessage(method, uri);
    requestMessage.Headers.Add("x-api-version", "1.0");
    return requestMessage;
  }

  private static HttpRequestMessage GetRequestMessage<T>(HttpMethod method, string uri, T requestBody)
  {
    HttpRequestMessage requestMessage = GetRequestMessage(method, uri);
    requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8,
      "application/json");
    return requestMessage;
  }
}