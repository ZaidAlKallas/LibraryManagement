using Library.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities;

public class BookCopy
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public bool IsAvailable { get; set; } = true;

    [StringLength(20)]
    public BookCondition Condition { get; set; } = BookCondition.Good;
}
