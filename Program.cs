using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectAveryFrontend;
using ProjectAveryFrontend.Logic.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TODO CKE replace with customizable backend port
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("http://localhost:35565") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IApplicationConnectionService, ApplicationConnectionService>();

await builder.Build().RunAsync();