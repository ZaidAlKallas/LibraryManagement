using Library.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddLibraryDbContext(builder.Configuration);

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Apply migrations automatically
app.ApplyMigrations();

app.MapControllers();
app.Run();
