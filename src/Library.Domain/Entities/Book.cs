using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities;
public class Book
{
    public int Id { get; set; }
    [Required, StringLength(80, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(13)]
    public string ISBN { get; set; } = string.Empty;
    [StringLength(200)]
    public string? Publisher { get; set; }
    [Display(Name = "Published Date")]
    public DateTime PublishedDate { get; set; }

    public ICollection<BookAuthor> Authors { get; set; } = null!;
    public ICollection<BookCopy> Copies { get; set; } = null!;

}
