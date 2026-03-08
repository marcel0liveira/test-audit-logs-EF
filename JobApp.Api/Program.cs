using JobApp.Infrastructure.Configurations.Builder;
using JobApp.Api.Configuration.Builder;
using JobApp.Api.Configuration.Pipeline;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();
builder.AddApi();

var app = builder.Build();

app.UseApi();

app.Run();
