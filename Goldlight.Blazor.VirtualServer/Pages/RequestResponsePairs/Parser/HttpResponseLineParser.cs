using System.Text.RegularExpressions;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

internal class HttpResponseLineParser
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