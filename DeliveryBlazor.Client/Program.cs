using DeliveryBlazor.Client.Services.UserClientService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DeliveryBlazor.Client.Services.OrderService;
using DeliveryBlazor.Client.Services.CourierService;
using DeliveryBlazor.Client.Services.ClientService;
using MudBlazor.Services;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddMudServices();
builder.Services.AddScoped<IUserServices , UserClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IClientServices, ClientService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7027/api/")
});



await builder.Build().RunAsync();

