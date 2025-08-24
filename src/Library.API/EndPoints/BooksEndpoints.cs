using Library.Application.Books.Queries.GetBooks;
using MediatR;

namespace Library.API.Endpoints;

public static class BooksEndpoints
{
    public static IEndpointRouteBuilder MapBooksEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/books").WithTags("Books");

        group.MapGet("/", async (IMediator mediator) =>
        {
            var books = await mediator.Send(new GetBooksQuery());
            return Results.Ok(books);
        });


        return app;
    }
}
