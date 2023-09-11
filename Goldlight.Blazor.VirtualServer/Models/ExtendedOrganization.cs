using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

[DataContract]
public class ExtendedOrganization
{
  [DataMember(Name = "id")]
  public string? Id { get; set; }

  [DataMember(Name = "name")]
  public string? Name { get; set; }

  [DataMember(Name = "version")]
  public long? Version { get; set; }

  [DataMember(Name = "apiKey")]
  public string? ApiKey { get; set; }
}