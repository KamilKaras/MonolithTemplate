using MonolithTemplate.Api.Extensions;
using MonolithTemplate.Identity.Api.Endpoints;
using MonolithTemplate.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//AddModules
builder.Services.AddIdentityModule(configuration);
//AddModules

builder.Services.AddGlobalExceptions();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunMigrations();
app.UseExceptionHandler();

//AddEndpoints
app.MapIdentityEndpoints();
//AddEndpoints

app.Run();


