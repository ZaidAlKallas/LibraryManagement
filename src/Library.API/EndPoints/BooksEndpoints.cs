using Library.Application.Books.Commands.AddBook;
using Library.Application.Books.Commands.DeleteBook;
using Library.Application.Books.Queries.GetBookById;
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

        group.MapGet("/{id:int}", async (int id, ISender sender) =>
        {
            var book = await sender.Send(new GetBookByIdQuery(id));
            return Results.Ok(book);
        })
        .WithName("GetBookById")
        .WithSummary("Get a book by id")
        .Produces<BookDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (IMediator mediator, AddBookCommand command) =>
        {
            var bookId = await mediator.Send(command);
            return Results.Created($"/api/books/{bookId}", new { Id = bookId });
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new DeleteBookCommand(id), ct);
            return Results.NoContent();
        })
        .WithName("DeleteBook")
        .WithSummary("Delete a book by id")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);


        return app;
    }
}
