using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class Response
{
  [Required, DataMember(Name = "summary")] public HttpResponseSummary Summary { get; set; } = new();
  [DataMember(Name = "headers")]
  public ObservableCollection<HttpHeader> Headers { get; set; } = new();

  [DataMember(Name = "content")]
  public string? Content { get; set; }
}