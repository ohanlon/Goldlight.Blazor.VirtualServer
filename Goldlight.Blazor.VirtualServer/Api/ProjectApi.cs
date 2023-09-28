using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Api;

public class ProjectApi
{
  private readonly HttpClient httpClient;

  public ProjectApi(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public async Task<List<Project>?> GetProjects(string organization) => 
    await httpClient.Get<List<Project>>($"api/{organization}/projects");

  public async Task<Project?> GetProject(string id) =>
    await httpClient.Get<Project>($"api/project/{id}");

  public async Task<Project?> SaveProject(Project project) =>
    await httpClient.Post<Project>("api/project", project);
}
