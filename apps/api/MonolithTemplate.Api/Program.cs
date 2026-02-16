using MonolithTemplate.Api.Extensions;
using MonolithTemplate.Identity.Api.Endpoints;
using MonolithTemplate.Identity.Infrastructure;
using MonolithTemplate.Notifications.Infrastructure;
using MonolithTemplate.Shared;

var builder = WebApplication.CreateBuilder(args);



IConfiguration configuration = builder.Configuration;

builder.Services.AddGlobalExceptions();

//AddModules
builder.Services.AddShared();
builder.Services.AddIdentityModule(configuration);
builder.Services.AddNotificationsModule(configuration);
//AddModules

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunMigrations();

app.UseExceptionHandler();

app.MapIdentityEndpoints();

app.Run();


