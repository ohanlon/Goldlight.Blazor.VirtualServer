using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class HttpHeader
{
  [Required, DataMember(Name = "name")] public string? Name { get; set; } = "Unset";
  [Required, DataMember(Name = "value")] public string? Value { get; set; } = "Unset";
}