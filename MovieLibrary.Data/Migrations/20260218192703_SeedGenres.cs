using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("25a1b61e-301b-4b95-b233-ff55721dbde6"), "Action" },
                    { new Guid("4ac2a016-a782-4103-aff2-9b651f145d92"), "Drama" },
                    { new Guid("7527dc1c-2c6d-420e-bcb9-03cad164d060"), "Horror" },
                    { new Guid("77680bbd-2bcb-42e1-91d7-ed69048b8af6"), "Comedy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("25a1b61e-301b-4b95-b233-ff55721dbde6"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4ac2a016-a782-4103-aff2-9b651f145d92"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7527dc1c-2c6d-420e-bcb9-03cad164d060"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("77680bbd-2bcb-42e1-91d7-ed69048b8af6"));
        }
    }
}
