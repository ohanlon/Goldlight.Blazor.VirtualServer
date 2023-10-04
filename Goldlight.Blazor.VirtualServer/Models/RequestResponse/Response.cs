using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Goldlight.Blazor.VirtualServer.Models.RequestResponse;

[DataContract]
public class Response
{
  [Required, DataMember] public HttpResponseSummary Summary { get; set; } = new();
  [DataMember]
  public ObservableCollection<HttpHeader> Headers { get; set; } = new();

  [DataMember]
  public string? Content { get; set; }
}