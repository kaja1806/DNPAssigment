using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWasm.Auth;
using BlazorWasm.Services;
using BlazorWasm.Services.Http;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

//WebAssembly host builder
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(
    sp =>
        new HttpClient
        {
            // BaseAddress for HttpClient 
            BaseAddress = new Uri("https://localhost:7015")
        }
);

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>(); //register the custom authentication state provider
builder.Services.AddScoped<IUserService, UserHttpClient>(); // register the UserHttpClient service
builder.Services.AddScoped<IAuthService, JwtAuthService>(); //register the JwtAuthService for authentication

AuthorizationPolicies.AddPolicies(builder.Services);

//added for adding posts/create functionality
builder.Services.AddScoped<IPostService, PostHttpClient>(); //register the PostHttpClient service for handling posts


await builder.Build().RunAsync();