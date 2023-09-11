using System.ComponentModel.DataAnnotations;

namespace Goldlight.Blazor.VirtualServer.Models;

internal class Project
{
  private string name = "";
  [Required]
  public string Name
  {
    get => name;
    set
    {
      name = value;
      FriendlyName = string.Join("",
        name.ToLowerInvariant().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }
  }

  public string FriendlyName { get; set; } = "";
  [Required] public string Description { get; set; } = "";
}