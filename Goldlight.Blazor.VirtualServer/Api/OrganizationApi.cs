using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Api;

public class OrganizationApi
{
  private readonly HttpClient httpClient;

  public OrganizationApi(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public async Task<ExtendedOrganization?> GetOrganizationAsync(string? name) =>
    await httpClient.Get<ExtendedOrganization>($"api/organization/name/{name}");
}