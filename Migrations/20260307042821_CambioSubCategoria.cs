using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioSubCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "Categorias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeObjetivo",
                table: "Categorias",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Categorias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Orden", "PorcentajeObjetivo", "Tipo" },
                values: new object[] { 0, 0m, "Gasto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PorcentajeObjetivo",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Categorias");
        }
    }
}
