using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanzasApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalGastos",
                table: "Meses",
                newName: "TransporteReal");

            migrationBuilder.RenameColumn(
                name: "TotalDeuda",
                table: "Meses",
                newName: "TransportePresupuesto");

            migrationBuilder.RenameColumn(
                name: "TotalAhorro",
                table: "Meses",
                newName: "OtrosReal");

            migrationBuilder.RenameColumn(
                name: "Patrimonio",
                table: "Meses",
                newName: "OtrosPresupuesto");

            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "Meses",
                newName: "Observaciones");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Inversiones",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "TasaEA",
                table: "Deudas",
                newName: "TipoTasa");

            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "Deudas",
                newName: "Tasa");

            migrationBuilder.RenameColumn(
                name: "Prioridad",
                table: "Deudas",
                newName: "PrioridadManual");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCierre",
                table: "Meses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<decimal>(
                name: "AhorroInversionPresupuesto",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AhorroInversionReal",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CasaFijosPresupuesto",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CasaFijosReal",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeudaPresupuesto",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeudaReal",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiversionPresupuesto",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiversionReal",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "EstaCerrado",
                table: "Meses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MesKey",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "AportesAcumulados",
                table: "Inversiones",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoActual",
                table: "Deudas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Empresa = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecioPromedio = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecioActual = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ahorros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    ValorActual = table.Column<decimal>(type: "TEXT", nullable: false),
                    MetaObjetivo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ahorros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuraciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Moneda = table.Column<string>(type: "TEXT", nullable: false),
                    IngresoBaseMensual = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcCasaFijosMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcCasaFijosMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcTransporteMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcTransporteMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcDeudaMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcDeudaMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcAhorroInversion = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcDiversionMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorcOtrosMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    UmbralAmarillo = table.Column<decimal>(type: "TEXT", nullable: false),
                    UmbralRojo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuraciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FondosEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Entidad = table.Column<string>(type: "TEXT", nullable: false),
                    ValorActual = table.Column<decimal>(type: "TEXT", nullable: false),
                    AporteMensual = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondosEmpleado", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Configuraciones",
                columns: new[] { "Id", "IngresoBaseMensual", "Moneda", "PorcAhorroInversion", "PorcCasaFijosMax", "PorcCasaFijosMin", "PorcDeudaMax", "PorcDeudaMin", "PorcDiversionMax", "PorcOtrosMax", "PorcTransporteMax", "PorcTransporteMin", "UmbralAmarillo", "UmbralRojo" },
                values: new object[] { 1, 0m, "COP", 7m, 28m, 23m, 22m, 18m, 3m, 8m, 5m, 4m, 95m, 105m });

            migrationBuilder.CreateIndex(
                name: "IX_Meses_MesKey",
                table: "Meses",
                column: "MesKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "Ahorros");

            migrationBuilder.DropTable(
                name: "Configuraciones");

            migrationBuilder.DropTable(
                name: "FondosEmpleado");

            migrationBuilder.DropIndex(
                name: "IX_Meses_MesKey",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "AhorroInversionPresupuesto",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "AhorroInversionReal",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "CasaFijosPresupuesto",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "CasaFijosReal",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "DeudaPresupuesto",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "DeudaReal",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "DiversionPresupuesto",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "DiversionReal",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "EstaCerrado",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "MesKey",
                table: "Meses");

            migrationBuilder.DropColumn(
                name: "AportesAcumulados",
                table: "Inversiones");

            migrationBuilder.DropColumn(
                name: "SaldoActual",
                table: "Deudas");

            migrationBuilder.RenameColumn(
                name: "TransporteReal",
                table: "Meses",
                newName: "TotalGastos");

            migrationBuilder.RenameColumn(
                name: "TransportePresupuesto",
                table: "Meses",
                newName: "TotalDeuda");

            migrationBuilder.RenameColumn(
                name: "OtrosReal",
                table: "Meses",
                newName: "TotalAhorro");

            migrationBuilder.RenameColumn(
                name: "OtrosPresupuesto",
                table: "Meses",
                newName: "Patrimonio");

            migrationBuilder.RenameColumn(
                name: "Observaciones",
                table: "Meses",
                newName: "Mes");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Inversiones",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "TipoTasa",
                table: "Deudas",
                newName: "TasaEA");

            migrationBuilder.RenameColumn(
                name: "Tasa",
                table: "Deudas",
                newName: "Saldo");

            migrationBuilder.RenameColumn(
                name: "PrioridadManual",
                table: "Deudas",
                newName: "Prioridad");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCierre",
                table: "Meses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
