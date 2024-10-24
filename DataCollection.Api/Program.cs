using DataCollection.Api.Endpoints;
using DataCollection.Api.ServiceRegistrations;
using DataCollection.Application.Handlers.QueryHandlers;
using DataCollection.Infrastructure.Middlewares;
using Formula1.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetVersionQueryHandler).Assembly));
builder.Services.AddExternalServices();
builder.Services.AddInfrastructureServices(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalHttpRequestMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapAliveEndpoints();
app.MapSearchEndpoints();

app.UseHttpsRedirection();

app.Run();

public partial class Program { } // For NUnit WebApplication integration tests
