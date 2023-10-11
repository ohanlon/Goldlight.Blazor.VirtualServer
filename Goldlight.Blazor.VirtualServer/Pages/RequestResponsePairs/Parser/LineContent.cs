using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public abstract class LineContent<T>
{
  public abstract bool HasSummary(T content, string line);

  public abstract void FillSummary(T content, string line);

  public void AddHeader(T content, HttpHeaderParser headerParser, string line)
  {
    var headerLine = headerParser.Parse(line);
    if (headerLine != null)
    {
      AddHeader(content, headerLine);
    }
  }

  public abstract void SetContent(T content, string lines);
  protected abstract void AddHeader(T content, HttpHeader headerLine);
}