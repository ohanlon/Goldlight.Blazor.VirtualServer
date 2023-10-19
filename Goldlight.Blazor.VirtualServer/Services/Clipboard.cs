using Microsoft.JSInterop;

namespace Goldlight.Blazor.VirtualServer.Services;

public class Clipboard
{
  private readonly IJSRuntime jsInterop;

  public Clipboard(IJSRuntime jsInterop)
  {
    this.jsInterop = jsInterop;
  }

  public async Task CopyToClipboard(string text)
  {
    await jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
  }
}