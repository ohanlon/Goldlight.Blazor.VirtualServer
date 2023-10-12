using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.State;

public class ProjectState
{
  public Project? SelectedProject { get; set; }
  public RequestResponsePair? SelectedRequestResponse { get; set; }
}
