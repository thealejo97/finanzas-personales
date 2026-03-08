using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioTasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Ahorros",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Ahorros",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TasaEA",
                table: "Ahorros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Ahorros");

            migrationBuilder.DropColumn(
                name: "TasaEA",
                table: "Ahorros");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Ahorros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "ActivoNombre", "CategoriaPadreId", "EsAhorro", "EsInversion", "Nombre", "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[,]
                {
                    { 11, null, null, false, false, "Ahorro", 0, 0m, "Gasto" },
                    { 12, null, 11, false, false, "Fondo emergencia", 0, 0m, "Gasto" },
                    { 13, null, 11, false, false, "Inversion", 0, 0m, "Gasto" }
                });

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
    }
}
