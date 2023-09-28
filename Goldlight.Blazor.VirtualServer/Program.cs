using Goldlight.Blazor.VirtualServer;
using Goldlight.Blazor.VirtualServer.Api;using Goldlight.Blazor.VirtualServer.Models;
using Goldlight.Blazor.VirtualServer.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["Server:BaseAddress"]!) });
builder.Services.AddMudServices();

builder.Services.AddScoped<OrganizationApi>().AddScoped<ProjectApi>().AddScoped<ProjectState>();

await builder.Build().RunAsync();
