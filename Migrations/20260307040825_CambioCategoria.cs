using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class CambioCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AhorroInversionPresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "AhorroInversionReal",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "CasaFijosPresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "CasaFijosReal",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "DeudaPresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "DeudaReal",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "DiversionPresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "DiversionReal",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "OtrosPresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "OtrosReal",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "TransportePresupuesto",
                table: "MesesFinancieros");

            migrationBuilder.DropColumn(
                name: "TransporteReal",
                table: "MesesFinancieros");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    CategoriaPadreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaPadreId",
                        column: x => x.CategoriaPadreId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presupuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MesFinancieroId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Presupuesto = table.Column<decimal>(type: "TEXT", nullable: false),
                    Real = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presupuestos_MesesFinancieros_MesFinancieroId",
                        column: x => x.MesFinancieroId,
                        principalTable: "MesesFinancieros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "CategoriaPadreId", "Nombre" },
                values: new object[,]
                {
                    { 1, null, "Casa" },
                    { 5, null, "Transporte" },
                    { 8, null, "Deudas" },
                    { 11, null, "Ahorro" },
                    { 2, 1, "Arriendo" },
                    { 3, 1, "Mercado" },
                    { 4, 1, "Servicios" },
                    { 6, 5, "Gasolina" },
                    { 7, 5, "Uber" },
                    { 9, 8, "Tarjeta Visa" },
                    { 10, 8, "Credito carro" },
                    { 12, 11, "Fondo emergencia" },
                    { 13, 11, "Inversion" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_CategoriaPadreId",
                table: "Categorias",
                column: "CategoriaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_CategoriaId",
                table: "Presupuestos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_MesFinancieroId",
                table: "Presupuestos",
                column: "MesFinancieroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.AddColumn<decimal>(
                name: "AhorroInversionPresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AhorroInversionReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CasaFijosPresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CasaFijosReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeudaPresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeudaReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiversionPresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiversionReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OtrosPresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtrosReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransportePresupuesto",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransporteReal",
                table: "MesesFinancieros",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
