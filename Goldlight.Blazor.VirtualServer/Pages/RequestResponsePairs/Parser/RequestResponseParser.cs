namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public abstract class RequestResponseParser<T, TK> where T : class, new()
  where TK : LineContent<T>, new()
{
  public T Parse(string file)
  {
    T response = new();
    string[] lines = file.Split(Environment.NewLine);
    bool headerParsed = false;
    HttpHeaderParser headerParser = new();

    TK lineContent = new();
    while (lines.Length > 0)
    {
      string line = lines[0];
      lines = lines.Skip(1).ToArray();
      if (lineContent.HasSummary(response, line))
      {
        lineContent.FillSummary(response, line);
        continue;
      }
      if (!headerParsed)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          headerParsed = true;
          continue;
        }
        lineContent.AddHeader(response, headerParser, line);
        continue;
      }

      // We want to ignore any blank lines that have inadvertently been added to the file, making sure we
      // have no content that shouldn't belong.
      if (string.IsNullOrWhiteSpace(line))
      {
        continue;
      }
      lineContent.SetContent(response, line + string.Concat(lines));
      break;
    }
    return response;
  }

}