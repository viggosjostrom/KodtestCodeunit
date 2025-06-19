using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KodtestCodeunit;
using KodtestCodeunit.Mappers;
using KodtestCodeunit.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<ApiService>();
await builder.Build().RunAsync();
