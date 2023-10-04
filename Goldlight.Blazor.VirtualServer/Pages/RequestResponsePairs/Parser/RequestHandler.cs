using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Parser;

public class RequestHandler
{
    public Request Parse(string file)
    {
        Request request = new();
        string[] lines = file.Split(Environment.NewLine);
        bool headerParsed = false;
        HttpHeaderParser headerParser = new();

        while (lines.Length > 0)
        {
            string line = lines[0];
            lines = lines.Skip(1).ToArray();
            if (string.IsNullOrWhiteSpace(request.Summary.Path) && !string.IsNullOrWhiteSpace(line))
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
                if (headerLine != null)
                {
                    request.Headers.Add(headerLine);
                }
                continue;
            }
            request.Content = line + string.Concat(lines);
            break;
        }
        return request;
    }

}
