using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using Goldlight.Blazor.VirtualServer.State;

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

    return new RequestResponsePair
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
          Protocol = pair.Request.Summary.Protocol
        }
      },
      Response = new Response()
      {
        Content = pair.Response.Content,
        Headers = responseHeaders,
        Summary = new HttpResponseSummary
        {
          Status = pair.Response.Summary.Status,
          Protocol = pair.Response.Summary.Protocol
        }
      }
    };
  }

  public static string UrlFriendlyPath(this RequestResponsePair pair) =>
    pair.Request.Summary.Path!.StartsWith("/") ? pair.Request.Summary.Path : $"/{pair.Request.Summary.Path}";

  public static string ServiceBaseUrl(this Project project, string baseUrl, OrganizationProps props) =>
    baseUrl.EndsWith("/")
      ? $"{baseUrl}{props.SelectedOrganization!.FriendlyName}/{project.FriendlyName}"
      : $"{baseUrl}/{props.SelectedOrganization!.FriendlyName}/{project.FriendlyName}";
}