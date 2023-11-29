using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

[DataContract]
public class Organization
{
  private string name = null!;
  [DataMember(Name = "id")] public Guid Id { get; set; }

  [DataMember(Name = "name"), MinLength(1), MaxLength(120), Required]
  public string Name
  {
    get => name;
    set
    {
      name = value;
      FriendlyName = WebUtility.UrlEncode(name.ToLowerInvariant());
    }
  }

  [DataMember(Name = "friendlyname"), MinLength(1), MaxLength(360), Required]
  public string FriendlyName { get; set; } = null!;

  [DataMember(Name = "version")] public long Version { get; set; }

  [DataMember(Name = "api-key"), MinLength(1), MaxLength(32)]
  public string ApiKey { get; set; }
}