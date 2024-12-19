using DeliveryBlazor.Infrastructure.Services.UserClientService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DeliveryBlazor.Infrastructure.Services.OrderService;
using DeliveryBlazor.Infrastructure.Services.CourierService;
using DeliveryBlazor.Infrastructure.Services.ClientService;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped<IUserServices , UserClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IClientServices, ClientService>();

await builder.Build().RunAsync();

