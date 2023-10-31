using Goldlight.Blazor.VirtualServer;
using Goldlight.Blazor.VirtualServer.Api;
using Goldlight.Blazor.VirtualServer.Services;
using Goldlight.Blazor.VirtualServer.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
  { BaseAddress = new Uri(builder.Configuration["Server:BaseAddress"]!) });
builder.Services.AddMudServices();

builder.Services.AddScoped<OrganizationApi>().AddScoped<ProjectApi>().AddScoped<ProjectProps>()
  .AddSingleton<Clipboard>();

builder.Services.AddOidcAuthentication(options => { builder.Configuration.Bind("Keycloak", options.ProviderOptions); });

await builder.Build().RunAsync();