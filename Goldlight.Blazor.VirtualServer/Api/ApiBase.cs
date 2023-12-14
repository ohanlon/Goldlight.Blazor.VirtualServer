using MudBlazor;

namespace Goldlight.Blazor.VirtualServer.Api;

public abstract class ApiBase
{
  private readonly ISnackbar snackbar;
  protected readonly HttpClient httpClient;

  protected ApiBase(HttpClient httpClient, ISnackbar snackbar)
  {
    this.httpClient = httpClient;
    this.snackbar = snackbar;
  }

  protected void SnackbarErrorMessage(string message)
  {
    snackbar.Add(message, Severity.Error);
  }

  protected void SnackbarWarning(string message)
  {
    snackbar.Add(message, Severity.Warning);
  }

  protected void SnackbarInfo(string message)
  {
    snackbar.Add(message, Severity.Info);
  }
}