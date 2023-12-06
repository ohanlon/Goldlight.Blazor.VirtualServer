using System.Collections.ObjectModel;
using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using Goldlight.Blazor.VirtualServer.State;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public static class ModelExtensions
{
  public static RequestResponsePair Clone(this RequestResponsePair pair) =>
    new()
    {
      Name = $"[CLONE] {pair.Name}",
      Description = pair.Description,
      ProjectId = pair.ProjectId,
      Version = 0,
      Request = new Request
      {
        Content = pair.Request.Content,
        Headers = CloneHeaders(pair.Request.Headers),
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
        Headers = CloneHeaders(pair.Response.Headers),
        Summary = new HttpResponseSummary
        {
          Status = pair.Response.Summary.Status,
          Protocol = pair.Response.Summary.Protocol
        }
      }
    };

  public static string UrlFriendlyPath(this RequestResponsePair pair) =>
    pair.Request.Summary.Path!.StartsWith("/") ? pair.Request.Summary.Path : $"/{pair.Request.Summary.Path}";

  public static string ServiceBaseUrl(this Project project, string baseUrl, OrganizationProps props) =>
    baseUrl.EndsWith("/")
      ? $"{baseUrl}{props.SelectedOrganization!.FriendlyName}/{project.FriendlyName}"
      : $"{baseUrl}/{props.SelectedOrganization!.FriendlyName}/{project.FriendlyName}";

  private static ObservableCollection<HttpHeader> CloneHeaders(ObservableCollection<HttpHeader>? headers)
  {
    ObservableCollection<HttpHeader> copyHeaders = new();
    if (headers is null)
    {
      return copyHeaders;
    }

    foreach (HttpHeader hdr in headers)
    {
      HttpHeader header = new() { Key = hdr.Key, Value = hdr.Value };
      copyHeaders.Add(header);
    }

    return copyHeaders;
  }
}