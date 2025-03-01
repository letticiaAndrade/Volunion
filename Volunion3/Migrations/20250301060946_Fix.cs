using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Responsabilidade",
                table: "AspNetUsers",
                newName: "Telefone");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "AspNetUsers",
                newName: "Responsabilidade");
        }
    }
}
