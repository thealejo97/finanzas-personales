using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Meses",
                table: "Meses");

            migrationBuilder.RenameTable(
                name: "Meses",
                newName: "MesesFinancieros");

            migrationBuilder.RenameIndex(
                name: "IX_Meses_MesKey",
                table: "MesesFinancieros",
                newName: "IX_MesesFinancieros_MesKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MesesFinancieros",
                table: "MesesFinancieros",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MesesFinancieros",
                table: "MesesFinancieros");

            migrationBuilder.RenameTable(
                name: "MesesFinancieros",
                newName: "Meses");

            migrationBuilder.RenameIndex(
                name: "IX_MesesFinancieros_MesKey",
                table: "Meses",
                newName: "IX_Meses_MesKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meses",
                table: "Meses",
                column: "Id");
        }
    }
}
