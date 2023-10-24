using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using System.Text.RegularExpressions;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class ResponseLineContent : LineContent<Response>
{
  public override bool SummaryIsMissing(Response response, string line) =>
    response.Summary.Status is null && !string.IsNullOrWhiteSpace(line);

  public override void SetContent(Response response, string lines) => response.Content = lines;

  protected override void AddHeader(Response response, HttpHeader headerLine)
  {
    response.Headers.Add(headerLine);
  }

  protected override void Parse(Response content, string requestLine)
  {
    HttpResponseSummary response = new();
    Match match = Regex.Match(requestLine.Trim(), @"(?<protocol>HTTP/\d+\.\d+)\s+(?<status>[\d]+)");
    if (match.Success)
    {
      int.TryParse(match.Groups["status"].Value, out int status);
      response.Protocol = match.Groups["protocol"].Value;
      response.Status = status;
    }

    content.Summary = response;
  }
}