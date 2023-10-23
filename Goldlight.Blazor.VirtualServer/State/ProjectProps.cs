using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.State;

public class ProjectProps
{
  public Project? SelectedProject { get; private set; }
  public RequestResponsePair? SelectedRequestResponse { get; private set; }

  public void Set(Project project, RequestResponsePair? requestResponsePair)
  {
    SelectedProject = project;
    SelectedRequestResponse = requestResponsePair;
  }

  public void Clear()
  {
    SelectedProject = null;
    SelectedRequestResponse = null;
  }
}