using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public static class ModelExtensions
{
  public static RequestResponsePair Clone(this RequestResponsePair pair)
  {
    ObservableCollection<HttpHeader> requestHeaders = new();
    foreach (HttpHeader hdr in pair.Request.Headers)
    {
      HttpHeader header = new() { Name = hdr.Name, Value = hdr.Value };
      requestHeaders.Add(header);
    }
    ObservableCollection<HttpHeader> responseHeaders = new();
    foreach (HttpHeader hdr in pair.Response.Headers)
    {
      HttpHeader header = new() { Name = hdr.Name, Value = hdr.Value };
      responseHeaders.Add(header);
    }

    return new()
    {
      Name = $"[CLONE] {pair.Name}",
      Description = pair.Description,
      Request = new Request
      {
        Content = pair.Request.Content,
        Headers = requestHeaders,
        Summary = new HttpRequestSummary
        {
          Method = pair.Request.Summary.Method,
          Path = pair.Request.Summary.Path,
          Version = pair.Request.Summary.Version
        }
      },
      Response = new Response()
      {
        Content = pair.Response.Content,
        Headers = responseHeaders,
        Summary = new HttpResponseSummary
        {
          Status = pair.Response.Summary.Status,
          Version = pair.Response.Summary.Version
        }
      }
    };
  }
}
