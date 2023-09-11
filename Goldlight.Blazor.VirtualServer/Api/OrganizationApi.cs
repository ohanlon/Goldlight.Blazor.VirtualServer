using System.Net.Http.Json;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Api;

public class OrganizationApi
{
  private readonly HttpClient httpClient;

  public OrganizationApi(HttpClient httpClient)
  {
    this.httpClient = httpClient;
    this.httpClient.BaseAddress = new Uri("http://localhost:5106/");
    this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-version", "1.0");
  } 

  public async Task<ExtendedOrganization?> GetOrganizationAsync(string? id)
  {
    var response = await httpClient.GetAsync($"api/organization/{id}");
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<ExtendedOrganization>();
  }
}
