using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectAveryFrontend;
using ProjectAveryFrontend.Logic.Services.Connections;
using ProjectAveryFrontend.Logic.Services.HttpsClients;
using ProjectAveryFrontend.Logic.Services.Managers;
using ProjectAveryFrontend.Logic.Services.Notifications;
using ProjectAveryFrontend.Logic.Services.Translation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Logging.AddFork(options => { });

//TODO CKE replace with customizable backend port
builder.Services.AddHttpClient<BackendClient>(client => client.BaseAddress = new Uri("http://localhost:35565"));
builder.Services.AddHttpClient<LocalClient>(client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
// All services are singletons so it's easier to use them and the whole Blazor App is one scope anyway
builder.Services.AddSingleton<IApplicationConnectionService, ApplicationConnectionService>();
builder.Services.AddSingleton<IEntityConnectionService, EntityConnectionService>();
builder.Services.AddSingleton<ITranslationService, DefaultTranslationService>();
builder.Services.AddSingleton<INotificationService, ApplicationNotificationService>();

builder.Services.AddSingleton<IApplicationStateManager, ApplicationStateManager>();


await builder.Build().RunAsync();