using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using MudBlazor;

namespace Goldlight.Blazor.VirtualServer.Api;

public class ProjectApi : ApiBase
{
  private readonly ResponseHandler responseHandler;

  public ProjectApi(HttpClient httpClient, ResponseHandler responseHandler, ISnackbar snackbar)
    : base(httpClient, snackbar)
  {
    this.responseHandler = responseHandler;
  }

  public async Task<ObservableCollection<Project>?> GetProjects(Guid organization) =>
    await httpClient.Get<ObservableCollection<Project>>($"api/{organization}/projects", responseHandler);

  public async Task<ObservableCollection<RequestResponsePair>?> GetAll(Guid organization, Guid project) =>
    await httpClient.Get<ObservableCollection<RequestResponsePair>>($"/api/{organization}/project/{project}/pairs",
      responseHandler);

  public async Task<Project?> GetProject(string id) =>
    await httpClient.Get<Project>($"api/project/{id}",
      responseHandler.NotFound(() =>
        SnackbarWarning("The project could not be found. It might have been deleted by another user.")));

  public async Task<Project?> SaveProject(Project project) =>
    await httpClient.Post("api/project", project, responseHandler
      .Created(() => SnackbarInfo("The project was successfully created"))
      .Ok(() => SnackbarInfo("The project has been updated")));

  public async Task<Project?> UpdateProject(Project project) =>
    await httpClient.Put("api/project", project,
      responseHandler.Conflict(() =>
        SnackbarErrorMessage("The project could not be saved as someone else has already changed it")));

  public async Task DeleteProject(Project project) =>
    await httpClient.Delete<string>($"api/{project.Organization}/project/{project.Id}",
      responseHandler.Ok(() => SnackbarInfo("The project has been deleted.")));

  public async Task<RequestResponsePair?> SaveRequestResponse(Guid organization, Guid projectId,
    RequestResponsePair rrpair) =>
    await httpClient.Post($"api/{organization}/project/{projectId}/rrpair", rrpair,
      responseHandler.Created(() => SnackbarInfo("The request/response pair was successfully created"))
        .Ok(() => SnackbarInfo("The request/response pair has been updated")));

  public async Task DeleteRequestResponse(Project project, RequestResponsePair pair) =>
    await httpClient.Delete<string>($"api/{project.Organization}/project/{project.Id}/rrpair/{pair.Id}",
      responseHandler.Ok(() => SnackbarInfo("The request/response pair has been deleted.")));
}