using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using System.Text.RegularExpressions;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class RequestLineContent : LineContent<Request>
{
  public override bool SummaryIsMissing(Request request, string line) =>
    string.IsNullOrWhiteSpace(request.Summary.Path) && !string.IsNullOrWhiteSpace(line);

  public override void SetContent(Request request, string lines) => request.Content = lines;

  protected override void AddHeader(Request request, HttpHeader headerLine)
  {
    request.Headers.Add(headerLine);
  }

  protected override void Parse(Request content, string requestLine)
  {
    HttpRequestSummary requestSummary = new();
    Match match = Regex.Match(requestLine.Trim(),
      @"(?<method>[\w]+)\s+(?<path>[\w\/%$-_.+!*'(),]+)(\s+)?(?<protocol>HTTP/\d+\.\d+)?");
    if (match.Success)
    {
      requestSummary.Method = match.Groups["method"].Value;
      requestSummary.Path = match.Groups["path"].Value;
      if (!requestSummary.Path.StartsWith("/"))
      {
        requestSummary.Path = "/" + requestSummary.Path;
      }

      requestSummary.Protocol = match.Groups["protocol"].Value;
    }

    content.Summary = requestSummary;
  }
}