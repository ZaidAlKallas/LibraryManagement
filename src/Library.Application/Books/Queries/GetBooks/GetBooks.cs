using Library.Application.Books.Queries.GetBookById;
using MediatR;

namespace Library.Application.Books.Queries.GetBooks;

public record GetBooksQuery() : IRequest<List<BookDto>>;
