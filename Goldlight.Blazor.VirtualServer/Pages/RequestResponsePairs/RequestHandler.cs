using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs;

public class RequestHandler
{
  public Request Parse(string file)
  {
    Request request = new ();
    string[] lines = file.Split(Environment.NewLine);
    bool headerParsed = false;
    HttpHeaderParser headerParser = new();

    while (lines.Length > 0)
    {
      string line = lines[0];
      lines = lines.Skip(1).ToArray();
      if (request.Summary is null && !string.IsNullOrWhiteSpace(line))
      {
        request.Summary = new HttpRequestLineParser().Parse(line);
        continue;
      }
      if (!headerParsed)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          headerParsed = true;
          continue;
        }
        var headerLine = headerParser.Parse(line);
        request.Headers.Add(headerLine);
        continue;
      }
      request.Content = string.Concat(lines);
      break;
    }
    return request;
  }

}
