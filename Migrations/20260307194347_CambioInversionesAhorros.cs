using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioInversionesAhorros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Inversiones",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Ahorros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Inversiones");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Ahorros");
        }
    }
}
