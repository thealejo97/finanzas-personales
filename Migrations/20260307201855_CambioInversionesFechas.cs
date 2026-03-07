using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioInversionesFechas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AportesAcumulados",
                table: "Inversiones",
                newName: "ValorActual");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorActual",
                table: "Inversiones",
                newName: "AportesAcumulados");
        }
    }
}
