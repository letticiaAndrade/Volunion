using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class FixCampaign2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campanhas_AspNetUsers_CriadorId",
                table: "Campanhas");

            migrationBuilder.DropIndex(
                name: "IX_Campanhas_CriadorId",
                table: "Campanhas");

            migrationBuilder.DropColumn(
                name: "CriadorId",
                table: "Campanhas");

            migrationBuilder.DropColumn(
                name: "VoluntariosInscritos",
                table: "Campanhas");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizacaoId",
                table: "Campanhas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CampanhaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campanhas_OrganizacaoId",
                table: "Campanhas",
                column: "OrganizacaoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Campanhas_AspNetUsers_OrganizacaoId",
                table: "Campanhas",
                column: "OrganizacaoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Campanhas_CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Campanhas_AspNetUsers_OrganizacaoId",
                table: "Campanhas");

            migrationBuilder.DropIndex(
                name: "IX_Campanhas_OrganizacaoId",
                table: "Campanhas");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CampanhaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizacaoId",
                table: "Campanhas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CriadorId",
                table: "Campanhas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VoluntariosInscritos",
                table: "Campanhas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Campanhas_CriadorId",
                table: "Campanhas",
                column: "CriadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campanhas_AspNetUsers_CriadorId",
                table: "Campanhas",
                column: "CriadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
