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

  public async Task<Organization?> GetOrganizationAsync(string? name) =>
    await httpClient.Get<Organization>($"api/organization/name/{name}");

  public async Task SaveOrganizationAsync(Organization organization) =>
    await httpClient.Post("api/organization", organization);
}