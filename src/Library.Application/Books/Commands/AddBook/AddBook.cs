using MediatR;

namespace Library.Application.Books.Commands.AddBook;

public record AddBookCommand(string Title, List<int> AuthorIds, string? Publisher) : IRequest<int>;
