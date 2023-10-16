using System.Text.RegularExpressions;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

internal class HttpResponseLineParser
{
  public static HttpResponseSummary Parse(string responseLine)
  {
    HttpResponseSummary response = new();
    Match match = Regex.Match(responseLine.Trim(), @"(?<protocol>HTTP/\d+\.\d+)\s+(?<status>[\d]+)");
    if (match.Success)
    {
      int.TryParse(match.Groups["status"].Value, out int status);
      response.Protocol = match.Groups["protocol"].Value;
      response.Status = status;
    }

    return response;
  }
}