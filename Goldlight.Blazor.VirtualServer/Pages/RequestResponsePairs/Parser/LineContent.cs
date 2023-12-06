using Goldlight.Blazor.VirtualServer.Models.RequestResponse;
using System.Text.RegularExpressions;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public abstract class LineContent<T>
{
  public abstract bool SummaryIsMissing(T content, string line);

  public void FillSummary(T content, string line)
  {
    Parse(content, line);
  }

  public bool HeaderParseCompleted;

  public void AddHeader(T content, string line)
  {
    HeaderParseCompleted = string.IsNullOrWhiteSpace(line);
    if (HeaderParseCompleted)
    {
      return;
    }

    AddHeader(content, ParseHeader(line));
  }

  public abstract void SetContent(T content, string lines);
  protected abstract void AddHeader(T content, HttpHeader headerLine);

  protected abstract void Parse(T content, string requestLine);

  private HttpHeader ParseHeader(string requestLine)
  {
    HttpHeader header = new();
    var match = Regex.Match(requestLine.Trim(), @"(?<name>[\w\-]+):\s+(?<value>.*)");
    {
      header.Key = match.Groups["name"].Value;
      header.Value = match.Groups["value"].Value;
    }
    return header;
  }
}