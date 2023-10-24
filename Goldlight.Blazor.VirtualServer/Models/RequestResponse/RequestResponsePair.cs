using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class RequestResponsePair
{
  [DataMember(Name = "name")] public string? Name { get; set; }
  [DataMember(Name = "description")] public string? Description { get; set; }
  [Required, DataMember(Name="request")] public Request Request { get; set; } = new();
  [Required, DataMember(Name="response")] public Response Response { get; set; } = new();
}