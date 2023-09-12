using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Goldlight.Blazor.VirtualServer.Models;

public class Project
{
  [Required]
  public string? Name { get; set; }
  [Required]
  public string? FriendlyName { get; set; }
  [Required] 
  public string? Description { get; set; }

  public string UrlName => FriendlyName is not null ? WebUtility.UrlEncode(FriendlyName) : "";
}