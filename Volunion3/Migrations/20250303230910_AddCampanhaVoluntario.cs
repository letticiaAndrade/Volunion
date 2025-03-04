using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class AddCampanhaVoluntario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Campanhas_CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c29e18e-0dcb-4bfa-aebf-48d7f46a5f1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b94c9d5-40f8-44ff-947c-5763fdf18a75");

            migrationBuilder.DropColumn(
                name: "CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CampanhaVoluntario",
                columns: table => new
                {
                    CampanhaId = table.Column<int>(type: "int", maxLength: 450, nullable: false),
                    VoluntarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    InscricaoData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampanhaVoluntario", x => new { x.CampanhaId, x.VoluntarioId });
                    table.ForeignKey(
                        name: "FK_CampanhaVoluntario_AspNetUsers_VoluntarioId",
                        column: x => x.VoluntarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CampanhaVoluntario_Campanhas_CampanhaId",
                        column: x => x.CampanhaId,
                        principalTable: "Campanhas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampanhaVoluntario_VoluntarioId",
                table: "CampanhaVoluntario",
                column: "VoluntarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampanhaVoluntario");

            migrationBuilder.AddColumn<int>(
                name: "CampanhaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c29e18e-0dcb-4bfa-aebf-48d7f46a5f1e", null, "organizacao", "organizacao" },
                    { "9b94c9d5-40f8-44ff-947c-5763fdf18a75", null, "voluntario", "organizacao" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CampanhaId",
                table: "AspNetUsers",
                column: "CampanhaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Campanhas_CampanhaId",
                table: "AspNetUsers",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id");
        }
    }
}
