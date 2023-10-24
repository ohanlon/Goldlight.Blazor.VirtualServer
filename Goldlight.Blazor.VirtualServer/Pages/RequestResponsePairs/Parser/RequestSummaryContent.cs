using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class RequestSummaryContent : LineContent<Request>
{
  public override bool SummaryIsMissing(Request request, string line) =>
    string.IsNullOrWhiteSpace(request.Summary.Path) && !string.IsNullOrWhiteSpace(line);

  public override void FillSummary(Request request, string line)
  {
    request.Summary = HttpRequestLineParser.Parse(line);
  }

  public override void SetContent(Request request, string lines) => request.Content = lines;

  protected override void AddHeader(Request request, HttpHeader headerLine)
  {
    request.Headers.Add(headerLine);
  }
}