using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Models;
using System.Collections.ObjectModel;

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

  public async Task<Project?> GetProject(string id) =>
    await httpClient.Get<Project>($"api/project/{id}");

  public async Task<Project?> SaveProject(Project project) =>
    await httpClient.Post("api/project", project);

  public async Task<Project?> UpdateProject(Project project) =>
    await httpClient.Put("/api/project", project);

  public async Task DeleteProject(string id) =>
    await httpClient.Delete<string>($"api/project/{id}");
}