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
    await HttpClient.Get<Organization>($"api/organization/name/{name}", responseHandler);

  public async Task SaveOrganizationAsync(Organization organization) =>
    await HttpClient.Post("api/organization", organization, responseHandler);

  public async Task<List<Organization>?> GetOrganizationsAsync() =>
    await HttpClient.Get<List<Organization>>("api/organizations", responseHandler);

  public async Task<ObservableCollection<OrganizationMember>?> GetOrganizationMembers(Organization organization) =>
    await HttpClient.Get<ObservableCollection<OrganizationMember>>($"api/organization/{organization.Id}/members",
      responseHandler);

  public async Task AddMemberToOrganizationAsync(Guid organization, OrganizationMember member) =>
    await HttpClient.Post<OrganizationMember>($"api/organization/{organization}/adduser", member,
      responseHandler.NotFound(() =>
          SnackbarWarning($"Could not add {member.EmailAddress}. Have you checked to make sure they are users?"))
        .Ok(() =>
        {
          member.EmailAddressIsLocked = true;
          member.EditEmailAddress = false;
        }));

  public async Task<bool> DoesUserHaveEditCapability(Guid organization) =>
    await HttpClient.Get<bool>($"api/{organization}/user/canedit", responseHandler);

  public async Task RemoveUserFromOrganization(Guid organization, string organizationMember) =>
    await HttpClient.Delete<Guid>($"api/{organization}/user/{organizationMember}", responseHandler);
}