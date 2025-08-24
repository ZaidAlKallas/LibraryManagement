using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Books.Queries.GetBooks;

public record GetBooksQuery() : IRequest<List<Book>>;
