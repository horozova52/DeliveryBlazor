﻿using DeliveryAppBlazor.Infrastructure.Contexts;
using DeliveryBlazor.Client.Services.UserClientService;
using DeliveryBlazor.Client.Services.CourierService;
using DeliveryBlazor.Client.Services.ClientService;
using DeliveryBlazor.Core.Entities;
using DeliveryBlazor.Server.Components.Account;
using DeliveryBlazor.Server.Components;
using DeliveryBlazor.UseCase.Features.ApplicationUsers.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using DeliveryBlazor.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationUserHandler>());
builder.Services.AddMudServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddHttpClient<UserClientService>();

builder.Services.AddHttpClient<UserClientService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]); // Set this in your appsettings.json
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IUserServices, UserClientService>();

builder.Services.AddScoped<ICourierService, CourierService>();

builder.Services.AddScoped<IClientServices, ClientService>();

// Configure Identity (DOAR ACEASTĂ CONFIGURARE)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, SmtpEmailSender>();
// Add MudBlazor services
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
        options.RoutePrefix = "swagger";
    });
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(DeliveryBlazor.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();

app.MapControllers();
app.Run();