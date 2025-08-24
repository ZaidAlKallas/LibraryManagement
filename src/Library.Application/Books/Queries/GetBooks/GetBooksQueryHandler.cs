using Library.Application.Common.Interfaces;
using Library.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries.GetBooks;

public class GetBooksQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBooksQuery, List<Book>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        => await _context.Books
            .Include(b => b.Authors)
            .ToListAsync(cancellationToken);
}
