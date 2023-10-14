using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped(
    sp => 
        new HttpClient { 
           
           // BaseAddress = new Uri("http://localhost:5187") 
            BaseAddress = new Uri("https://localhost:7015") 
       }
);

builder.Services.AddScoped<IUserService, UserHttpClient>();


//aded for add Post/ create
builder.Services.AddScoped<IPostService, PostHttpClient>();


await builder.Build().RunAsync();