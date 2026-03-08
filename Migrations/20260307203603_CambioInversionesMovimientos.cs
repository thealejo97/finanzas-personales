using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioInversionesMovimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivoNombre",
                table: "Categorias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsAhorro",
                table: "Categorias",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsInversion",
                table: "Categorias",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ActivoNombre", "EsAhorro", "EsInversion" },
                values: new object[] { null, false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivoNombre",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "EsAhorro",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "EsInversion",
                table: "Categorias");
        }
    }
}
