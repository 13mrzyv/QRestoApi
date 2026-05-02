using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QResto.Blazor;
using QResto.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<BasketService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<OrderEntryService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7001/") });

await builder.Build().RunAsync();
