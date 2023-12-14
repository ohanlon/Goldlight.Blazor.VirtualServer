using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;
using Goldlight.Blazor.VirtualServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public class TokenAuthorizationMessageHandler : DelegatingHandler
{
  private readonly Uri baseUri;
  private readonly IAccessTokenProvider accessTokenProvider;
  private AuthenticationHeaderValue? cachedAuthenticationHeaderValue;
  private AccessToken? cachedAccessToken;
  private readonly NavigationManagement navigationManagement;

  public TokenAuthorizationMessageHandler(IAccessTokenProvider provider,
    IConfiguration configuration, NavigationManagement navigationManagement)
  {
    baseUri = new Uri(configuration["Server:BaseAddress"]!);
    accessTokenProvider = provider;
    this.navigationManagement = navigationManagement;
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    if (baseUri.IsBaseOf(request.RequestUri!))
    {
      await PopulateCachedAccessTokenIfUnsetOrExpired();
      request.Headers.Authorization = cachedAuthenticationHeaderValue!;
    }

    return await base.SendAsync(request, cancellationToken);
  }

  private async Task PopulateCachedAccessTokenIfUnsetOrExpired()
  {
    if (cachedAccessToken is null || cachedAccessToken.Expires.AddMinutes(-5) <= DateTime.Now)
    {
      cachedAccessToken = null;
      AccessToken? accessToken = await RequestAuthToken(accessTokenProvider);
      if (accessToken is not null)
      {
        cachedAccessToken = accessToken;
        cachedAuthenticationHeaderValue = new AuthenticationHeaderValue("Bearer", accessToken.Value);
      }
    }
  }

  private async Task<AccessToken?> RequestAuthToken(IAccessTokenProvider tokenProvider)
  {
    AccessTokenResult? requestToken = await tokenProvider.RequestAccessToken();
    if (!requestToken.TryGetToken(out AccessToken? token))
    {
      navigationManagement.Login();
    }

    return token;
  }
}