namespace Library.Application.Books.Queries.GetBookById;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = [];
}
