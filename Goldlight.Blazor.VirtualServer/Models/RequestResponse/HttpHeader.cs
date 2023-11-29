using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class HttpHeader
{
  [DataMember(Name = "headerid")] public Guid Id { get; set; }

  [Required, DataMember(Name = "name"), MinLength(1), MaxLength(120)]
  public string Name { get; set; } = "Unset";

  [Required, DataMember(Name = "value"), MinLength(1), MaxLength(1024)]
  public string Value { get; set; } = "Unset";
}