using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class HttpRequestSummary
{
  [Required, DataMember(Name = "method")]
  public string Method { get; set; } = "ANY";

  [Required, DataMember(Name = "path")] public string? Path { get; set; }
  [DataMember(Name = "version")] public string? Version { get; set; }
}