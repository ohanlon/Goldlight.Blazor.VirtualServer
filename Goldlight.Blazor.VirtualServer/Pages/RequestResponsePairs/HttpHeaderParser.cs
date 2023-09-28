using System.Text.RegularExpressions;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs;

public class HttpHeaderParser
{
  public HttpHeader? Parse(string requestLine)
  {
    if (string.IsNullOrEmpty(requestLine)) return null;
    HttpHeader header = new();
    var match = Regex.Match(requestLine.Trim(), @"(?<name>[\w\-]+):\s+(?<value>.*)");
    {
      header.Name = match.Groups["name"].Value;
      header.Value = match.Groups["value"].Value;
    }
    return header;
  }
}