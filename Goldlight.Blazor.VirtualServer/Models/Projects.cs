namespace Goldlight.Blazor.VirtualServer.Models;

internal class Projects : List<Project>
{
  public Projects()
  {
    Add(new Project { Name = "Code Project", FriendlyName = "codeproject", Description = "Code Project APIs" });
    Add(new Project { Name = "Goldlight Samples", FriendlyName = "goldlight", Description = "Goldlight APIs" });
  }
}