using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeDeleteToCampanhaVoluntario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampanhaVoluntario_AspNetUsers_VoluntarioId",
                table: "CampanhaVoluntario");

            migrationBuilder.DropForeignKey(
                name: "FK_CampanhaVoluntario_Campanhas_CampanhaId",
                table: "CampanhaVoluntario");

            migrationBuilder.AddForeignKey(
                name: "FK_CampanhaVoluntario_AspNetUsers_VoluntarioId",
                table: "CampanhaVoluntario",
                column: "VoluntarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CampanhaVoluntario_Campanhas_CampanhaId",
                table: "CampanhaVoluntario",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampanhaVoluntario_AspNetUsers_VoluntarioId",
                table: "CampanhaVoluntario");

            migrationBuilder.DropForeignKey(
                name: "FK_CampanhaVoluntario_Campanhas_CampanhaId",
                table: "CampanhaVoluntario");

            migrationBuilder.AddForeignKey(
                name: "FK_CampanhaVoluntario_AspNetUsers_VoluntarioId",
                table: "CampanhaVoluntario",
                column: "VoluntarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampanhaVoluntario_Campanhas_CampanhaId",
                table: "CampanhaVoluntario",
                column: "CampanhaId",
                principalTable: "Campanhas",
                principalColumn: "Id");
        }
    }
}
