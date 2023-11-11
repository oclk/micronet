using IdentityService.Api.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

// Use all feature(s)
app.Use();

app.Run();
