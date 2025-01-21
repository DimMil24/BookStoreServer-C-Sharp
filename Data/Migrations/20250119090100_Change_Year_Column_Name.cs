using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreServerNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_Year_Column_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "year",
                table: "Books",
                newName: "published_year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "published_year",
                table: "Books",
                newName: "year");
        }
    }
}
