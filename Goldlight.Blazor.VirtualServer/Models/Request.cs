using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

public class RequestResponsePair
{
  [Required, MinLength(10), DataMember] public string? Name { get; set; } 
  [Required, MinLength(20), DataMember] public string? Description { get; set; }
  [Required, DataMember] public Request? Request = new();
  [Required, DataMember] public Response? Response = new();
}

public class Response
{
  [Required, DataMember] public HttpResponseSummary Summary { get; set; } = new();
  [DataMember]
  public List<HttpHeader> Headers { get; set; } = new();

  [DataMember]
  public string? Content { get; set; }
}

public class Request
{
  [Required, DataMember] public HttpRequestSummary Summary { get; set; } = new();

  [DataMember]
  public List<HttpHeader> Headers { get; set; } = new();

  [DataMember]
  public string? Content { get; set; }
}