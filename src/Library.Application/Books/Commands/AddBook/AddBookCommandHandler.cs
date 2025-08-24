using Library.Application.Common.Interfaces;
using Library.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.AddBook;

public class AddBookCommandHandler(IApplicationDbContext context)
    : IRequestHandler<AddBookCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(AddBookCommand request,
        CancellationToken cancellationToken)
    {
        var existingAuthorIds = await _context.Authors
            .Where(a => request.AuthorIds.Contains(a.Id))
            .AsNoTracking()
            .Select(a => a.Id)
            .ToListAsync(cancellationToken);

        var missing = request.AuthorIds.Except(existingAuthorIds).ToList();
        if (missing.Count > 0)
        {
            throw new ArgumentException($"Authors not found: {string.Join(", ", missing)}");
        }

        var book = new Book
        {
            Title = request.Title.Trim(),
            Publisher = request.Publisher
        };

        foreach (var authorId in existingAuthorIds.Distinct())
        {
            book.Authors.Add(new BookAuthor { AuthorId = authorId });
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);
        return book.Id;
    }
}
