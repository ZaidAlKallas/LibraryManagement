using Library.API.Endpoints;
using Library.API.Extensions;
using Library.API.Middleware;
using Library.Application;
using Library.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddSwaggerDocumentation();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddApplication();
// Add DbContext
builder.Services.AddLibraryDbContext(builder.Configuration);

var app = builder.Build();

// Apply migrations automatically
app.ApplyMigrations();
// Middleware
app.UseSwaggerDocumentation();
app.UseExceptionHandler();

app.MapBooksEndpoints();
app.Run();
