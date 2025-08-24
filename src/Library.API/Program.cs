using Library.API.Endpoints;
using Library.Application.Books.Queries.GetBooks;
using Library.Infrastructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddLibraryDbContext(builder.Configuration);

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetBooksQuery))!));

var app = builder.Build();

// Apply migrations automatically
app.ApplyMigrations();

app.MapBooksEndpoints();
app.Run();
