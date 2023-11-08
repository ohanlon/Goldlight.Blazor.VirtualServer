using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public static class HttpClientExtensions
{
  public static async Task<T?> Get<T>(this HttpClient client, string uri, ResponseHandler? handler = null) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Get, uri), handler);

  public static async Task<T?> Post<T>(this HttpClient client, string uri, T requestBody,
    ResponseHandler? handler = null) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Post, uri, requestBody), handler);

  public static async Task<T?> Put<T>(this HttpClient client, string uri, T requestBody,
    ResponseHandler? handler = null) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Put, uri, requestBody), handler);

  public static async Task Delete<T>(this HttpClient client, string uri, ResponseHandler? handler = null) =>
    await GetResponseFromServer<T>(client, GetRequestMessage(HttpMethod.Delete, uri), handler);

  private static async Task<T?> GetResponseFromServer<T>(HttpClient client, HttpRequestMessage requestMessage,
    ResponseHandler? handler)
  {
    HttpResponseMessage response = await client.SendAsync(requestMessage);
    switch (response.StatusCode)
    {
      case HttpStatusCode.Conflict:
        handler?.Conflict();
        break;
      case HttpStatusCode.NotFound:
        handler?.NotFound();
        break;
      case HttpStatusCode.OK:
        handler?.Ok();
        return await response.Content.ReadFromJsonAsync<T>();
      case HttpStatusCode.Created:
        handler?.Created();
        return await response.Content.ReadFromJsonAsync<T>();
      case HttpStatusCode.Unauthorized:
        handler?.Unauthorized();
        break;
      case HttpStatusCode.Forbidden:
        handler?.Forbidden();
        break;
      default:
        handler?.ServerFailure();
        break;
    }

    return default;
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