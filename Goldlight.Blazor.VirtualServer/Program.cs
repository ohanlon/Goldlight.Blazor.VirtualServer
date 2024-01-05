using Goldlight.Blazor.VirtualServer;
using Goldlight.Blazor.VirtualServer.Api;
using Goldlight.Blazor.VirtualServer.Extensions;
using Goldlight.Blazor.VirtualServer.Services;
using Goldlight.Blazor.VirtualServer.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ServerAPI",
    client => client.BaseAddress = new Uri(builder.Configuration["Server:BaseAddress"]!))
  .AddHttpMessageHandler<TokenAuthorizationMessageHandler>();

builder.Services.AddScoped<TokenAuthorizationMessageHandler>()
  .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("ServerAPI"));

builder.Services.AddMudServices();
builder.Services.AddScoped<OrganizationApi>().AddScoped<ProjectApi>().AddScoped<ProjectProps>()
  .AddSingleton<Clipboard>().AddScoped<OrganizationProps>().AddScoped<ResponseHandler>()
  .AddScoped<NavigationManagement>();

builder.Services.AddOidcAuthentication(options =>
{
  builder.Configuration.Bind("Keycloak", options.ProviderOptions);
  options.UserOptions.NameClaim = "preferred_username";
  options.UserOptions.RoleClaim = "roles";
  options.UserOptions.ScopeClaim = "scope";
});

builder.Services.AddMudServices(config =>
{
  config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

  config.SnackbarConfiguration.PreventDuplicates = true;
  config.SnackbarConfiguration.NewestOnTop = true;
  config.SnackbarConfiguration.ShowCloseIcon = true;
  config.SnackbarConfiguration.VisibleStateDuration = 3000;
  config.SnackbarConfiguration.HideTransitionDuration = 200;
  config.SnackbarConfiguration.ShowTransitionDuration = 200;
  config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
  config.SnackbarConfiguration.ClearAfterNavigation = true;
});

builder.Services.AddScoped<ISnackbar, SnackbarService>();

await builder.Build().RunAsync();