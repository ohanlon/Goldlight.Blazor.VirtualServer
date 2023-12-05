using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Models;

[DataContract]
public class Project
{
  [DataMember(Name = "id")] public Guid Id { get; set; }

  [Required, DataMember(Name = "organization_id")]
  public Guid Organization { get; set; } = Guid.Empty;

  [Required, DataMember(Name = "name"), MinLength(1), MaxLength(120)]
  public string Name { get; set; } = null!;

  [Required, DataMember(Name = "friendlyname"), MinLength(1), MaxLength(120)]
  public string FriendlyName { get; set; } = null!;

  [Required, DataMember(Name = "description")]
  public string? Description { get; set; }

  [Required, DataMember(Name = "version")]
  public long Version { get; set; }

  public ObservableCollection<RequestResponsePair>? RequestResponses { get; set; }
}