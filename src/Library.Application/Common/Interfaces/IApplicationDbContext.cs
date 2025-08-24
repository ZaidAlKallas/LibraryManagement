using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Author> Authors { get; }
    DbSet<BookAuthor> BookAuthors { get; }
    DbSet<BookCopy> BookCopies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
