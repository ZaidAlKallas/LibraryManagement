using MediatR;

namespace Library.Application.Books.Commands.DeleteBook;

public record DeleteBookCommand(int Id) : IRequest<Unit>;
