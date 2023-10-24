using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class Request
{
  [Required, DataMember(Name = "summary")]
  public HttpRequestSummary Summary { get; set; } = new();

  [DataMember(Name = "headers")] public ObservableCollection<HttpHeader> Headers { get; set; } = new();

  [DataMember(Name = "content")] public string? Content { get; set; }
}