using MonolithTemplate.Api.Extensions;
using MonolithTemplate.Identity.Api.Endpoints;
using MonolithTemplate.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;


builder.Services.AddIdentityModule(configuration);

builder.Services.AddGlobalExceptions();

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


