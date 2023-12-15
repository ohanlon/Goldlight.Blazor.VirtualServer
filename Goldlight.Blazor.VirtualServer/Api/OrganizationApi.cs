using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;
using MudBlazor;

namespace Goldlight.Blazor.VirtualServer.Api;

public class OrganizationApi : ApiBase
{
  private readonly ResponseHandler responseHandler;

  public OrganizationApi(HttpClient httpClient, ResponseHandler responseHandler, ISnackbar snackbar)
    : base(httpClient, snackbar)
  {
    this.responseHandler = responseHandler;
  }

  public async Task<Organization?> GetOrganizationAsync(string? name) =>
    await httpClient.Get<Organization>($"api/organization/name/{name}", responseHandler);

  public async Task SaveOrganizationAsync(Organization organization) =>
    await httpClient.Post("api/organization", organization, responseHandler);

  public async Task<List<Organization>?> GetOrganizationsAsync() =>
    await httpClient.Get<List<Organization>>("api/organizations", responseHandler);

  public async Task<ObservableCollection<OrganizationMember>?> GetOrganizationMembers(Organization organization) =>
    await httpClient.Get<ObservableCollection<OrganizationMember>>($"api/organization/{organization.Id}/members",
      responseHandler);

  public async Task AddMemberToOrganizationAsync(Guid organization, OrganizationMember member) =>
    await httpClient.Post<OrganizationMember>($"api/organization/{organization}/adduser", member,
      responseHandler.NotFound(() =>
          SnackbarWarning($"Could not add {member.EmailAddress}. Have you checked to make sure they are users?"))
        .Ok(() => member.EditEmailAddress = false));
}