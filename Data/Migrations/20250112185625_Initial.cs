using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreServerNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    isbn13 = table.Column<long>(type: "INTEGER", nullable: false),
                    isbn10 = table.Column<string>(type: "TEXT", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    authors = table.Column<string>(type: "TEXT", nullable: true),
                    categories = table.Column<string>(type: "TEXT", nullable: true),
                    thumbnail = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    year = table.Column<int>(type: "INTEGER", nullable: true),
                    average_rating = table.Column<double>(type: "REAL", nullable: true),
                    num_pages = table.Column<int>(type: "INTEGER", nullable: true),
                    ratings_count = table.Column<int>(type: "INTEGER", nullable: true),
                    price = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.isbn13);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
