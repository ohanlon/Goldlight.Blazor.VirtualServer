using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models;

public class RequestResponsePair
{
  [Required, DataMember] public Request Request = new();
  [Required, DataMember] public Response Response = new();
}

public class Response
{
  [Required, DataMember] public HttpResponseSummary? Summary { get; set; }
  [DataMember]
  public List<HttpHeader> Headers { get; set; } = new();

  [DataMember]
  public string? Content { get; set; }
}

public class Request
{
  [Required, DataMember] public HttpRequestSummary? Summary { get; set; }

  [DataMember]
  public List<HttpHeader> Headers { get; set; } = new();

  [DataMember]
  public string? Content { get; set; }
}