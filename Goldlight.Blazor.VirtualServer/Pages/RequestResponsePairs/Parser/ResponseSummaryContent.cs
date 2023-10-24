using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class ResponseSummaryContent : LineContent<Response>
{
  public override bool SummaryIsMissing(Response response, string line) =>
    response.Summary.Status is null && !string.IsNullOrWhiteSpace(line);

  public override void FillSummary(Response response, string line)
  {
    response.Summary = HttpResponseLineParser.Parse(line.Trim());
  }

  public override void SetContent(Response response, string lines) => response.Content = lines;

  protected override void AddHeader(Response response, HttpHeader headerLine)
  {
    response.Headers.Add(headerLine);
  }
}