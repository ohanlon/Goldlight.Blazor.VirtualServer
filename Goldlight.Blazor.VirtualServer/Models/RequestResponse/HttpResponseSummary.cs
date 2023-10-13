using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class HttpResponseSummary
{
  [DataMember(Name="version"), Required] public string? Version { get; set; }
  [DataMember(Name = "status"), Required] public int? Status { get; set; }
}