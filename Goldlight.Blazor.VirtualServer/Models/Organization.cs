using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

[DataContract]
public class Organization
{
  private string name = null!;
  [DataMember(Name = "id")] public Guid Id { get; set; } = Guid.Empty;

  [DataMember(Name = "name"), MaxLength(120), Required]
  public string Name
  {
    get => name;
    set
    {
      name = value;
      FriendlyName = WebUtility.UrlEncode(name.ToLowerInvariant());
    }
  }

  [DataMember(Name = "friendlyname"), MaxLength(360), Required]
  public string FriendlyName { get; set; } = null!;

  [DataMember(Name = "version")] public long Version { get; set; }

  [DataMember(Name = "api-key")] public string ApiKey { get; set; }
}