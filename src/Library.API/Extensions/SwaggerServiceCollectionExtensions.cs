using Microsoft.OpenApi.Models;

namespace Library.API.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library Management API",
                Version = "v1",
                Description = "API for managing books, authors, and related operations.",
                Contact = new OpenApiContact
                {
                    Name = "Library API Team",
                    Email = "support@library.local"
                }
            });
        });

        return services;
    }
}
