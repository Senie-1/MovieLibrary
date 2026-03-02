using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2abc5e18-4f6b-4d98-8f20-c3661ce52bc1"), "Action" },
                    { new Guid("8bb6f05b-eca9-4b07-a221-68189445c18a"), "Comedy" },
                    { new Guid("e105e170-0b28-4795-babe-7aee440cf829"), "Drama" },
                    { new Guid("e8ee178a-51c0-4814-80f6-03b370635bd8"), "Horror" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2abc5e18-4f6b-4d98-8f20-c3661ce52bc1"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8bb6f05b-eca9-4b07-a221-68189445c18a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e105e170-0b28-4795-babe-7aee440cf829"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e8ee178a-51c0-4814-80f6-03b370635bd8"));

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");

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
    }
}
