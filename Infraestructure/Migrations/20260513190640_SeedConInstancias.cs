using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedConInstancias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EVENT",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final Universitaria de Software", "Active", "Auditorio UNAJ" });

            migrationBuilder.InsertData(
                table: "SECTOR",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "VIP", 25000.00m },
                    { 2, 50, 1, "Preferencial", 12500.00m }
                });

            migrationBuilder.InsertData(
                table: "SEAT",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Fila 1", 1, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Fila 1", 2, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Fila 1", 3, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Fila 1", 4, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Fila 1", 5, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Fila 1", 6, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Fila 1", 7, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Fila 1", 8, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Fila 1", 9, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Fila 1", 10, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Fila 2", 11, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Fila 2", 12, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Fila 2", 13, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Fila 2", 14, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Fila 2", 15, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Fila 2", 16, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Fila 2", 17, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Fila 2", 18, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Fila 2", 19, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Fila 2", 20, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Fila 3", 21, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Fila 3", 22, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Fila 3", 23, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Fila 3", 24, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Fila 3", 25, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Fila 3", 26, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Fila 3", 27, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "Fila 3", 28, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Fila 3", 29, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Fila 3", 30, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Fila 4", 31, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Fila 4", 32, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "Fila 4", 33, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "Fila 4", 34, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "Fila 4", 35, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "Fila 4", 36, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "Fila 4", 37, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "Fila 4", 38, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "Fila 4", 39, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "Fila 4", 40, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "Fila 5", 41, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "Fila 5", 42, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "Fila 5", 43, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "Fila 5", 44, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "Fila 5", 45, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "Fila 5", 46, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "Fila 5", 47, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "Fila 5", 48, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "Fila 5", 49, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "Fila 5", 50, 1, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "Fila 1", 51, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "Fila 1", 52, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "Fila 1", 53, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "Fila 1", 54, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "Fila 1", 55, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "Fila 1", 56, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "Fila 1", 57, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "Fila 1", 58, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "Fila 1", 59, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000060"), "Fila 1", 60, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000061"), "Fila 2", 61, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000062"), "Fila 2", 62, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000063"), "Fila 2", 63, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000064"), "Fila 2", 64, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000065"), "Fila 2", 65, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000066"), "Fila 2", 66, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000067"), "Fila 2", 67, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000068"), "Fila 2", 68, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000069"), "Fila 2", 69, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000070"), "Fila 2", 70, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000071"), "Fila 3", 71, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000072"), "Fila 3", 72, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000073"), "Fila 3", 73, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000074"), "Fila 3", 74, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000075"), "Fila 3", 75, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000076"), "Fila 3", 76, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000077"), "Fila 3", 77, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000078"), "Fila 3", 78, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000079"), "Fila 3", 79, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000080"), "Fila 3", 80, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000081"), "Fila 4", 81, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000082"), "Fila 4", 82, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000083"), "Fila 4", 83, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000084"), "Fila 4", 84, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000085"), "Fila 4", 85, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000086"), "Fila 4", 86, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000087"), "Fila 4", 87, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000088"), "Fila 4", 88, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000089"), "Fila 4", 89, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000090"), "Fila 4", 90, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000091"), "Fila 5", 91, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000092"), "Fila 5", 92, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000093"), "Fila 5", 93, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000094"), "Fila 5", 94, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000095"), "Fila 5", 95, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000096"), "Fila 5", 96, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000097"), "Fila 5", 97, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000098"), "Fila 5", 98, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000099"), "Fila 5", 99, 2, "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000100"), "Fila 5", 100, 2, "Available", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"));

            migrationBuilder.DeleteData(
                table: "SECTOR",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SECTOR",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
