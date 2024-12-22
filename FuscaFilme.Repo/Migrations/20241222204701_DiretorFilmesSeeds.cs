using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FuscaFilme.Repo.Migrations
{
    /// <inheritdoc />
    public partial class DiretorFilmesSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DiretoresFilmes",
                columns: new[] { "DiretorId", "FilmeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 4, 10 },
                    { 4, 11 },
                    { 4, 12 },
                    { 5, 13 },
                    { 5, 14 },
                    { 5, 15 },
                    { 6, 16 },
                    { 6, 17 },
                    { 6, 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 5, 13 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 5, 14 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 5, 15 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 6, 16 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 6, 17 });

            migrationBuilder.DeleteData(
                table: "DiretoresFilmes",
                keyColumns: new[] { "DiretorId", "FilmeId" },
                keyValues: new object[] { 6, 18 });
        }
    }
}
