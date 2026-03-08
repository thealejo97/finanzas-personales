using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioInversionesRelacionadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Inversiones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Ahorros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inversiones_CategoriaId",
                table: "Inversiones",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ahorros_CategoriaId",
                table: "Ahorros",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ahorros_Categorias_CategoriaId",
                table: "Ahorros",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inversiones_Categorias_CategoriaId",
                table: "Inversiones",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ahorros_Categorias_CategoriaId",
                table: "Ahorros");

            migrationBuilder.DropForeignKey(
                name: "FK_Inversiones_Categorias_CategoriaId",
                table: "Inversiones");

            migrationBuilder.DropIndex(
                name: "IX_Inversiones_CategoriaId",
                table: "Inversiones");

            migrationBuilder.DropIndex(
                name: "IX_Ahorros_CategoriaId",
                table: "Ahorros");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Inversiones");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Ahorros");
        }
    }
}
