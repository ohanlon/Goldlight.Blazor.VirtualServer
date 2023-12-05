using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;
using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Api;

public class ProjectApi
{
  private readonly HttpClient httpClient;

  public ProjectApi(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public async Task<ObservableCollection<Project>?> GetProjects(Guid organization) =>
    await httpClient.Get<ObservableCollection<Project>>($"api/{organization}/projects");

  public async Task<ObservableCollection<RequestResponsePair>?> GetAll(Guid organization, Guid project) =>
    await httpClient.Get<ObservableCollection<RequestResponsePair>>($"/api/{organization}/project/{project}/pairs");

  public async Task<Project?> GetProject(string id) =>
    await httpClient.Get<Project>($"api/project/{id}");

  public async Task<Project?> SaveProject(Project project) =>
    await httpClient.Post("api/project", project);

  public async Task<Project?> UpdateProject(Project project) =>
    await httpClient.Put("api/project", project);

  public async Task DeleteProject(string id) =>
    await httpClient.Delete<string>($"api/project/{id}");

  public async Task<RequestResponsePair?> SaveRequestResponse(Guid projectId, RequestResponsePair rrpair) =>
    await httpClient.Post($"api/{projectId}/rrpair", rrpair);
}