using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52a4eec6-696c-43fd-9c6d-5bd475aa5275");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72e99d02-865f-403d-8408-95c1adcbd17e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c29e18e-0dcb-4bfa-aebf-48d7f46a5f1e", null, "organizacao", "ORGANIZACAO" },
                    { "9b94c9d5-40f8-44ff-947c-5763fdf18a75", null, "voluntario", "VOLUNTARIO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c29e18e-0dcb-4bfa-aebf-48d7f46a5f1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b94c9d5-40f8-44ff-947c-5763fdf18a75");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52a4eec6-696c-43fd-9c6d-5bd475aa5275", null, "voluntario", "voluntario" },
                    { "72e99d02-865f-403d-8408-95c1adcbd17e", null, "organizacao", "organizacao" }
                });
        }
    }
}
