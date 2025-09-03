using FluentValidation.TestHelper;
using Library.Application.Books.Commands.AddBook;

namespace Library.Application.Tests.Books.Commands;

public class AddBookCommandValidatorTests
{
    private readonly AddBookCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new AddBookCommand("", "1234567890", [1], "TestPublisher");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Title);
    }

    [Fact]
    public void Should_Have_Error_When_ISBN_Is_Empty()
    {
        var command = new AddBookCommand("Test Title", "", [1], "TestPublisher");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.ISBN);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var command = new AddBookCommand("Clean Code", "9780132350884", [1], "Prentice Hall");

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
