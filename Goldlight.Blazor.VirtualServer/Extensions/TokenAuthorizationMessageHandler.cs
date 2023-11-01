using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;

namespace Goldlight.Blazor.VirtualServer.Extensions;

public class TokenAuthorizationMessageHandler : DelegatingHandler
{
  private readonly Uri baseUri;
  private readonly IAccessTokenProvider accessTokenProvider;
  private AuthenticationHeaderValue? cachedAuthenticationHeaderValue;
  private AccessToken? cachedAccessToken;

  public TokenAuthorizationMessageHandler(IAccessTokenProvider provider,
    IConfiguration configuration)
  {
    baseUri = new Uri(configuration["Server:BaseAddress"]!);
    accessTokenProvider = provider;
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
      AccessToken? accessToken = await RequestAuthToken(accessTokenProvider);
      if (accessToken is not null)
      {
        cachedAccessToken = accessToken;
        cachedAuthenticationHeaderValue = new AuthenticationHeaderValue("Bearer", accessToken.Value);
      }
    }
  }

  private static async Task<AccessToken?> RequestAuthToken(IAccessTokenProvider tokenProvider)
  {
    AccessTokenResult? requestToken = await tokenProvider.RequestAccessToken();
    requestToken.TryGetToken(out AccessToken? token);
    return token;
  }
}