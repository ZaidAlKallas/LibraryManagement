using Library.API.Endpoints;
using Library.API.Extensions;
using Library.API.Middleware;
using Library.Application.Books.Queries.GetBooks;
using Library.Infrastructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddSwaggerDocumentation();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
// Add DbContext
builder.Services.AddLibraryDbContext(builder.Configuration);

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetBooksQuery))!));

var app = builder.Build();

// Apply migrations automatically
app.ApplyMigrations();
// Middleware
app.UseSwaggerDocumentation();
app.UseExceptionHandler();

app.MapBooksEndpoints();
app.Run();
