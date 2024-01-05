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
    await HttpClient.Get<ObservableCollection<Project>>($"api/{organization}/projects", responseHandler);

  public async Task<ObservableCollection<RequestResponsePair>?> GetAll(Guid organization, Guid project) =>
    await HttpClient.Get<ObservableCollection<RequestResponsePair>>($"/api/{organization}/project/{project}/pairs",
      responseHandler);

  public async Task<Project?> GetProject(string id) =>
    await HttpClient.Get<Project>($"api/project/{id}",
      responseHandler.NotFound(() =>
        SnackbarWarning("The project could not be found. It might have been deleted by another user.")));

  public async Task<Project?> SaveProject(Project project) =>
    await HttpClient.Post("api/project", project, responseHandler
      .Created(() => SnackbarInfo("The project was successfully created"))
      .Ok(() => SnackbarInfo("The project has been updated")));

  public async Task<Project?> UpdateProject(Project project) =>
    await HttpClient.Put("api/project", project,
      responseHandler.Conflict(() =>
        SnackbarErrorMessage("The project could not be saved as someone else has already changed it")));

  public async Task DeleteProject(Project project) =>
    await HttpClient.Delete<string>($"api/{project.Organization}/project/{project.Id}",
      responseHandler.Ok(() => SnackbarInfo("The project has been deleted.")));

  public async Task<RequestResponsePair?> SaveRequestResponse(Guid organization, Guid projectId,
    RequestResponsePair rrpair) =>
    await HttpClient.Post($"api/{organization}/project/{projectId}/rrpair", rrpair,
      responseHandler.Created(() => SnackbarInfo("The request/response pair was successfully created"))
        .Ok(() => SnackbarInfo("The request/response pair has been updated")));

  public async Task DeleteRequestResponse(Project project, RequestResponsePair pair) =>
    await HttpClient.Delete<string>($"api/{project.Organization}/project/{project.Id}/rrpair/{pair.Id}",
      responseHandler.Ok(() => SnackbarInfo("The request/response pair has been deleted.")));
}