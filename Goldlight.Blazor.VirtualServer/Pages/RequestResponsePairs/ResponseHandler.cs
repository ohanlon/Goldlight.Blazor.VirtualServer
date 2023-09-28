﻿using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs;

public class ResponseHandler
{
  public Response Parse(string file)
  {
    Response response = new();
    string[] lines = file.Split(Environment.NewLine);
    bool headerParsed = false;
    HttpHeaderParser headerParser = new();

    while (lines.Length > 0)
    {
      string line = lines[0];
      lines = lines.Skip(1).ToArray();
      if (response.Summary is null && !string.IsNullOrWhiteSpace(line))
      {
        response.Summary = new HttpResponseLineParser().Parse(line);
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
        response.Headers.Add(headerLine);
        continue;
      }
      response.Content = string.Concat(lines);
      break;
    }
    return response;
  }
}