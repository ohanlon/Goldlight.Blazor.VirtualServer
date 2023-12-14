using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace Goldlight.Blazor.VirtualServer.Services;

public class NavigationManagement
{
  private readonly NavigationManager navigationManager;
  private readonly IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> optionsSnapshot;

  public NavigationManagement(NavigationManager navigationManager,
    IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> optionsSnapshot)
  {
    this.navigationManager = navigationManager;
    this.optionsSnapshot = optionsSnapshot;
  }

  public void Logout()
  {
    navigationManager.NavigateToLogout("authentication/logout");
  }

  public void Login()
  {
    navigationManager.NavigateToLogin(optionsSnapshot.Get(Options.DefaultName).AuthenticationPaths.LogInPath);
  }
}