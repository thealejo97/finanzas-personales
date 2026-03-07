using FinanzasApp.Data;
using FinanzasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzasApp.Services;

public class FinanzasService
{
    private readonly FinanzasDbContext _db;

    public FinanzasService(FinanzasDbContext db)
    {
        _db = db;
    }

    // -------------------------
    // Inicialización
    // -------------------------

    public async Task InicializarDatosBaseAsync()
    {
        await _db.Database.EnsureCreatedAsync();
    }

    // -------------------------
    // MES ACTUAL
    // -------------------------

public async Task<MesFinanciero> ObtenerOCrearMesAsync(string mesKey)
{
    var mes = await _db.MesesFinancieros
        .Include(x => x.Presupuestos)
        .ThenInclude(x => x.Categoria)
        .FirstOrDefaultAsync(x => x.MesKey == mesKey);

    if (mes == null)
    {
        mes = new MesFinanciero
        {
            MesKey = mesKey
        };

        _db.MesesFinancieros.Add(mes);
        await _db.SaveChangesAsync();
    }

    // Obtener subcategorías
    var subCategorias = await _db.Categorias
        .Where(x => x.CategoriaPadreId != null)
        .ToListAsync();

    // Verificar si falta alguna
    foreach (var cat in subCategorias)
    {
        if (!mes.Presupuestos.Any(x => x.CategoriaId == cat.Id))
        {
            _db.Presupuestos.Add(new PresupuestoCategoria
            {
                MesFinancieroId = mes.Id,
                CategoriaId = cat.Id,
                Presupuesto = 0,
                Real = 0
            });
        }
    }

    await _db.SaveChangesAsync();

    return await _db.MesesFinancieros
        .Include(x => x.Presupuestos)
        .ThenInclude(x => x.Categoria)
        .ThenInclude(x => x.CategoriaPadre)
        .FirstAsync(x => x.Id == mes.Id);
}
    // -------------------------
    // GUARDAR MES
    // -------------------------

public async Task GuardarMesAsync(MesFinanciero mes)
{
    var existente = await _db.MesesFinancieros
        .Include(x => x.Presupuestos)
        .FirstAsync(x => x.Id == mes.Id);

    existente.Ingreso = mes.Ingreso;

    foreach (var presupuesto in mes.Presupuestos)
    {
        var existentePresupuesto = existente.Presupuestos
            .FirstOrDefault(x => x.Id == presupuesto.Id);

        if (existentePresupuesto == null)
            continue;

        existentePresupuesto.Presupuesto = presupuesto.Presupuesto;
        existentePresupuesto.Real = presupuesto.Real;
    }

    await _db.SaveChangesAsync();
}
    // -------------------------
    // CERRAR MES
    // -------------------------

    public async Task CerrarMesAsync(int mesId)
    {
        var mes = await _db.MesesFinancieros
            .FirstAsync(x => x.Id == mesId);

        mes.EstaCerrado = true;
        mes.FechaCierre = DateTime.Now;

        await _db.SaveChangesAsync();
    }

    // -------------------------
    // HISTORIAL
    // -------------------------

    public async Task<List<MesFinanciero>> ObtenerHistorialAsync()
    {
        return await _db.MesesFinancieros
            .OrderByDescending(x => x.MesKey)
            .ToListAsync();
    }

    // -------------------------
    // DASHBOARD
    // -------------------------

    public async Task<DashboardResumen> ObtenerDashboardAsync()
    {
        var inversiones = (await _db.Inversiones.ToListAsync())
            .Sum(x => x.ValorActual);

        var deudas = (await _db.Deudas.ToListAsync())
            .Sum(x => x.SaldoActual);

        var ahorros = (await _db.Ahorros.ToListAsync())
            .Sum(x => x.ValorActual);

        var fondos = (await _db.FondosEmpleado.ToListAsync())
            .Sum(x => x.ValorActual);

        var acciones = (await _db.Acciones.ToListAsync())
            .Sum(x => x.ValorActual);

        return new DashboardResumen
        {
            InversionesTotal = inversiones,
            DeudaTotal = deudas,
            AhorroTotal = ahorros,
            FondoEmpleadoTotal = fondos,
            AccionesTotal = acciones,
            PatrimonioEstimado =
                inversiones +
                ahorros +
                fondos +
                acciones -
                deudas
        };
    }

    // -------------------------
    // DEUDAS
    // -------------------------

    public async Task<List<Deuda>> ObtenerDeudasOrdenadasAsync()
    {
        return await _db.Deudas
            .OrderByDescending(x => x.Tasa)
            .ToListAsync();
    }

    public async Task GuardarDeudaAsync(Deuda deuda)
    {
        if (deuda.Id == 0)
            _db.Deudas.Add(deuda);
        else
            _db.Deudas.Update(deuda);

        await _db.SaveChangesAsync();
    }

    public decimal CalcularInteresMensualDeuda(Deuda deuda)
    {
        var tasaMensual = Math.Pow(1 + (double)deuda.Tasa / 100, 1.0 / 12.0) - 1;

        return deuda.SaldoActual * (decimal)tasaMensual;
    }

    public int CalcularMesesSalidaDeuda(Deuda deuda)
    {
        if (deuda.PagoObjetivo <= 0)
            return 0;

        var saldo = deuda.SaldoActual;
        var tasa = deuda.Tasa / 100 / 12;

        int meses = 0;

        while (saldo > 0 && meses < 600)
        {
            var interes = saldo * tasa;
            saldo = saldo + interes - deuda.PagoObjetivo;
            meses++;
        }

        return meses;
    }
}