using Library.Application.Common.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLibraryDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbProvider = configuration["DatabaseProvider"] ?? "SqlServer";

        services.AddDbContext<LibraryDbContext>(options =>
        {
            if (dbProvider == "SqlServer")
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            else if (dbProvider == "Sqlite")
                options.UseSqlite(configuration.GetConnectionString("Sqlite"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<LibraryDbContext>());

        return services;
    }
}
