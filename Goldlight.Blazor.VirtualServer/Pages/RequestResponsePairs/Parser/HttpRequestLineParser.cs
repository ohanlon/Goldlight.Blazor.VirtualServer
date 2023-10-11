using System.Text.RegularExpressions;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

internal class HttpRequestLineParser
{
    public HttpRequestSummary Parse(string requestLine)
    {
        HttpRequestSummary request = new();
        Match match = Regex.Match(requestLine.Trim(), @"(?<method>[\w]+)\s+(?<path>[\w\/]+)(\s+)?(?<version>HTTP/\d+\.\d+)?");
        if (match.Success)
        {
            request.Method = match.Groups["method"].Value;
            request.Path = match.Groups["path"].Value;
            if (!request.Path.StartsWith("/"))
            {
              request.Path = "/" + request.Path;
            }
            request.Version = match.Groups["version"].Value;
        }
        return request;
    }
}