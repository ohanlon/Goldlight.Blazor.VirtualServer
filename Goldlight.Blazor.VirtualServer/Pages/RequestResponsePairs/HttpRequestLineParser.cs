using System.Text.RegularExpressions;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs;

public class HttpRequestLineParser
{
    public HttpRequestSummary Parse(string requestLine)
    {
        HttpRequestSummary request = new();
        Match match = Regex.Match(requestLine.Trim(), @"(?<method>[\w]+)\s+(?<path>[\w\/]+)(\s+)?(?<version>HTTP/\d+\.\d+)?");
        if (match.Success)
        {
            request.Method = match.Groups["method"].Value;
            request.Path = match.Groups["path"].Value;
            request.Version = match.Groups["version"].Value;
        }
        return request;
    }
}

public class HttpResponseLineParser
{
  public HttpResponseSummary Parse(string responseLine)
  {
    HttpResponseSummary response = new();
    Match match = Regex.Match(responseLine.Trim(), @"(?<version>HTTP/\d+\.\d+)\s+(?<status>[\d]+)");
    if (match.Success)
    {
      response.Version = match.Groups["version"].Value;
      response.Status = match.Groups["status"].Value;
    }

    return response;
  }
}