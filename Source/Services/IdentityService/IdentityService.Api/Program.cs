using IdentityService.Api.Common.Extensions;
using IdentityService.Application.Common.Extensions;
using IdentityService.Infrastructure.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Use all feature(s)
app.Use();

app.Run();
