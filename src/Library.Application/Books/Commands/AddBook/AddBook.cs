using MediatR;

namespace Library.Application.Books.Commands.AddBook;

public record AddBookCommand(string Title, string ISBN, List<int> AuthorIds, string? Publisher) : IRequest<int>;
