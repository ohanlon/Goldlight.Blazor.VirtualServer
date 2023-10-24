using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class HttpResponseSummary
{
  [DataMember(Name="protocol"), Required] public string? Protocol { get; set; }
  [DataMember(Name = "status"), Required] public int? Status { get; set; }
}