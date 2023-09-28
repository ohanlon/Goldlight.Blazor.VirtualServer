using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

public class Project
{
  [DataMember]
  public Guid Id { get; set; }

  [Required, DataMember]
  public string? Organization { get; set; }
  [Required, DataMember]
  public string? Name { get; set; }
  [Required, DataMember]
  public string? FriendlyName { get; set; }
  [Required, DataMember] public List<RequestResponsePair> RequestResponses { get; set; } = new();
  [Required, DataMember]
  public string? Description { get; set; }
  [DataMember(Name = "version")]
  public long? Version { get; set; }

  public string UrlName => FriendlyName is not null ? WebUtility.UrlEncode(FriendlyName.ToLowerInvariant()) : "";

}