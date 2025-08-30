using FluentValidation;

namespace Library.Application.Books.Commands.AddBook;

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.")
            .MaximumLength(200).WithMessage("Publisher cannot exceed 200 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{3}-\d{10}$").WithMessage("ISBN format must be 978-XXXXXXXXXX.");

        RuleFor(x => x.AuthorIds)
            .NotNull().WithMessage("Authors are required.")
            .Must(ids => ids.Count > 0).WithMessage("At least one author is required.");
    }
}
