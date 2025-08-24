using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities;
public class Author
{
    public int Id { get; set; }
    [StringLength(70, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    [StringLength(500)]
    public string Bio { get; set; } = string.Empty;

    public ICollection<BookAuthor> Books { get; set; } = null!;
}
