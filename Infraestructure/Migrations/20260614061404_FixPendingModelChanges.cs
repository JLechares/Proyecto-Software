using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Venue" },
                values: new object[] { "Maria Becerra Concierto", "River Plate Stadium" });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "Juanlechares@gmail.com", "Juan", "1232132" },
                    { 2, "Juanmerino@gmail.com", "Juan Cruz", "5555555" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Venue" },
                values: new object[] { "Final Universitaria de Software", "Auditorio UNAJ" });
        }
    }
}
