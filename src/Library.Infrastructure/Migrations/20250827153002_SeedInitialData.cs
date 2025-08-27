using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Name" },
                values: new object[,]
                {
                    { 1, "Author of C# in a Nutshell.", "Joseph Albahari" },
                    { 2, "Expert in ASP.NET Core and web development.", "Adam Freeman" },
                    { 3, "Co-author of Introduction to Algorithms.", "Thomas H. Cormen" },
                    { 4, "Uncle Bob, author of Clean Code & Clean Architecture.", "Robert C. Martin" },
                    { 5, "Author of Data Structures and Algorithms Made Easy.", "Narasimha Karumanchi" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "ISBN", "PublishedDate", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "9781098158702", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "O'Reilly Media", "C# 12 in a Nutshell" },
                    { 2, "9781484292277", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apress", "Pro ASP.NET Core 8" },
                    { 3, "9780262046305", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MIT Press", "Introduction to Algorithms" },
                    { 4, "9788193245279", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CareerMonk", "Data Structures and Algorithms Made Easy" },
                    { 5, "9780132350884", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prentice Hall", "Clean Code" },
                    { 6, "9780134494166", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pearson", "Clean Architecture" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 5, 4 },
                    { 4, 5 },
                    { 4, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
