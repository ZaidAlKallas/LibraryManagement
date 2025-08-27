using Library.Application.Common.Interfaces;
using Library.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Unit> Handle(
        UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken) ??
            throw new KeyNotFoundException($"Book with id {request.Id} not found.");

        // Update scalar properties
        book.Title = request.Title;
        book.Publisher = request.Publisher;

        // Update authors (clear then re-add)
        var authors = await _context.Authors
            .Where(a => request.AuthorIds.Contains(a.Id))
            .ToListAsync(cancellationToken);

        if (authors.Count != request.AuthorIds.Count)
            throw new ValidationException("Some authors not found.");

        book.Authors.Clear();
        foreach (var author in authors)
        {
            book.Authors.Add(new BookAuthor
            {
                AuthorId = author.Id,
                BookId = book.Id,
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
