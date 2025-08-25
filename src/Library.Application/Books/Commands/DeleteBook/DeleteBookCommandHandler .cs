using Library.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken) ??
            throw new KeyNotFoundException($"Book with id {request.Id} not found.");

        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
