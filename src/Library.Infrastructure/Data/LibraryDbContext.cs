using Library.Application.Common.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;
public class LibraryDbContext(DbContextOptions options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookCopy> BookCopies => Set<BookCopy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Book
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        // BookAuthor many-to-many
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(ba => ba.AuthorId);

        // Enum conversion (BookCondition → string)
        var maxLength = Enum.GetNames<BookCondition>()
                    .Max(name => name.Length);
        modelBuilder.Entity<BookCopy>()
            .Property(c => c.Condition)
            .HasConversion<string>()
            .HasMaxLength(maxLength);

        SeedingData(modelBuilder);

    }
    private static void SeedingData(ModelBuilder modelBuilder)
    {
        // ==== Authors ====
        var authors = new List<Author>
        {
            new() { Id = 1, Name = "Joseph Albahari", Bio = "Author of C# in a Nutshell." },
            new() { Id = 2, Name = "Adam Freeman", Bio = "Expert in ASP.NET Core and web development." },
            new() { Id = 3, Name = "Thomas H. Cormen", Bio = "Co-author of Introduction to Algorithms." },
            new() { Id = 4, Name = "Robert C. Martin", Bio = "Uncle Bob, author of Clean Code & Clean Architecture." },
            new() { Id = 5, Name = "Narasimha Karumanchi", Bio = "Author of Data Structures and Algorithms Made Easy." }
        };

        modelBuilder.Entity<Author>().HasData(authors);

        // ==== Books ====
        var books = new List<Book>
        {
            new() { Id = 1, Title = "C# 12 in a Nutshell", Publisher = "O'Reilly Media", ISBN = "978-1098158702" },
            new() { Id = 2, Title = "Pro ASP.NET Core 8", Publisher = "Apress", ISBN = "978-1484292277" },
            new() { Id = 3, Title = "Introduction to Algorithms", Publisher = "MIT Press", ISBN = "978-0262046305" },
            new() { Id = 4, Title = "Data Structures and Algorithms Made Easy", Publisher = "CareerMonk", ISBN = "978-8193245279" },
            new() { Id = 5, Title = "Clean Code", Publisher = "Prentice Hall", ISBN = "978-0132350884" },
            new() { Id = 6, Title = "Clean Architecture", Publisher = "Pearson", ISBN = "978-0134494166" }
        };

        modelBuilder.Entity<Book>().HasData(books);

        // ==== BookAuthor (Many-to-Many links) ====
        var bookAuthors = new List<BookAuthor>
        {
            new() { BookId = 1, AuthorId = 1 },// C#
            new() { BookId = 2, AuthorId = 2 },// ASP.NET Core
            new() { BookId = 3, AuthorId = 3 },// Algorithms
            new() { BookId = 4, AuthorId = 5 },// Data Structures
            new() { BookId = 5, AuthorId = 4 },// Clean Code
            new() { BookId = 6, AuthorId = 4 },// Clean Architecture
        };

        modelBuilder.Entity<BookAuthor>().HasData(bookAuthors);
    }

}
