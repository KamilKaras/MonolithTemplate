using MonolithTemplate.Api.Extensions;
using MonolithTemplate.Api.Extensions.Auth;
using MonolithTemplate.Api.Extensions.CORS;
using MonolithTemplate.Identity.Api.Endpoints;
using MonolithTemplate.Identity.Infrastructure;
using MonolithTemplate.Notifications.Infrastructure;
using MonolithTemplate.Shared;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddGlobalExceptions();
builder.Services.AddAppCors(configuration);
builder.Services.AddAppAuth(configuration);

//AddModules
builder.Services.AddShared();
builder.Services.AddIdentityModule(configuration);
builder.Services.AddNotificationsModule(configuration);
//AddModules

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

await app.RunMigrations();

app.MapIdentityEndpoints();

app.Run();


