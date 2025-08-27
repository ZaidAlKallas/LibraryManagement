using MediatR;

namespace Library.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand(
    int Id,
    string Title,
    string Publisher,
    List<int> AuthorIds
) : IRequest<Unit>;
