using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunion3.Migrations
{
    /// <inheritdoc />
    public partial class FixOrganizationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizacaoId",
                table: "Campanhas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizacaoId",
                table: "Campanhas");
        }
    }
}
