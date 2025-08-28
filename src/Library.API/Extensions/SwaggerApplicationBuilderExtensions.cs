namespace Library.API.Extensions;

public static class SwaggerApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
            options.RoutePrefix = string.Empty; // Swagger يفتح مباشرة على الـ root
        });

        return app;
    }
}
