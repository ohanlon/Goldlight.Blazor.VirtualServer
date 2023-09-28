using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

public class HttpResponseSummary
{
  [DataMember, Required] public string? Version { get; set; }
  [DataMember, Required] public string? Status { get; set; }
}

public class HttpRequestSummary
{
  [Required, MinLength(10), DataMember] public string? Name { get; set; }
  [Required, MinLength(20), DataMember] public string? Description { get; set; }
  [Required, DataMember] public string Method { get; set; } = "";
  [Required, DataMember] public string Path { get; set; } = "";
  [DataMember] public string? Version { get; set; }
}

public class HttpHeader
{
  [Required, DataMember] public string Name { get; set; } = "";
  [Required, DataMember] public string Value { get; set; } = "";
}