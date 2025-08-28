using Library.Application.Books.Queries.GetBookById;
using Library.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries.GetBooks;

public class GetBooksQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        => await _context.Books
        .AsNoTracking()
        .Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Publisher = b.Publisher ?? "Not found",
            Authors = b.Authors.Select(a => a.Author!.Name).ToList(),
        })
        .ToListAsync(cancellationToken);
}
