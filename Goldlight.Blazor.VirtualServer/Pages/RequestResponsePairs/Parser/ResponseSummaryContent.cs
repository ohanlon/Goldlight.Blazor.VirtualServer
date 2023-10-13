using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class ResponseSummaryContent : LineContent<Response>
{
  public override bool HasSummary(Response response, string line) => response.Summary.Status is not null && !string.IsNullOrWhiteSpace(line);

  public override void FillSummary(Response response, string line)
  {
    response.Summary = new HttpResponseLineParser().Parse(line);
  }

  public override void SetContent(Response response, string lines) => response.Content = lines;

  protected override void AddHeader(Response response, HttpHeader headerLine)
  {
    response.Headers.Add(headerLine);
  }
}