using Library.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<BookDto> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken) ??
            throw new KeyNotFoundException($"Book with id {request.Id} not found.");

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Publisher = book.Publisher ?? string.Empty,
            Authors = [.. book.Authors.Select(ba => ba.Author?.Name)]
        };
    }
}
