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
    var mesExistente = await _db.MesesFinancieros
        .AsNoTracking()
        .Include(x => x.Presupuestos)
        .ThenInclude(x => x.Categoria)
        .ThenInclude(x => x.CategoriaPadre)
        .FirstOrDefaultAsync(x => x.MesKey == mesKey);

    if (mesExistente == null)
    {
        var nuevoMes = new MesFinanciero
        {
            MesKey = mesKey,
            Ingreso = 0,
            EstaCerrado = false,
            FechaCierre = null
        };

        _db.MesesFinancieros.Add(nuevoMes);
        await _db.SaveChangesAsync();

        var subCategorias = await _db.Categorias
            .Where(x => x.CategoriaPadreId != null)
            .ToListAsync();

        foreach (var cat in subCategorias)
        {
            _db.Presupuestos.Add(new PresupuestoCategoria
            {
                MesFinancieroId = nuevoMes.Id,
                CategoriaId = cat.Id,
                Presupuesto = 0,
                Real = 0
            });
        }

        await _db.SaveChangesAsync();
    }
    else
    {
        var subCategorias = await _db.Categorias
            .Where(x => x.CategoriaPadreId != null)
            .ToListAsync();

        var categoriasExistentes = mesExistente.Presupuestos
            .Select(x => x.CategoriaId)
            .ToHashSet();

        bool hayNuevas = false;

        foreach (var cat in subCategorias)
        {
            if (!categoriasExistentes.Contains(cat.Id))
            {
                _db.Presupuestos.Add(new PresupuestoCategoria
                {
                    MesFinancieroId = mesExistente.Id,
                    CategoriaId = cat.Id,
                    Presupuesto = 0,
                    Real = 0
                });

                hayNuevas = true;
            }
        }

        if (hayNuevas)
            await _db.SaveChangesAsync();
    }

    return await _db.MesesFinancieros
        .AsNoTracking()
        .Include(x => x.Presupuestos)
        .ThenInclude(x => x.Categoria)
        .ThenInclude(x => x.CategoriaPadre)
        .FirstAsync(x => x.MesKey == mesKey);
}

    // -------------------------
    // GUARDAR MES
    // -------------------------

public async Task GuardarMesAsync(MesFinanciero mes)
{
    var existente = await _db.MesesFinancieros
        .Include(x => x.Presupuestos)
        .ThenInclude(x => x.Categoria)
        .FirstAsync(x => x.Id == mes.Id);

    existente.Ingreso = mes.Ingreso;

    foreach (var presupuestoEditado in mes.Presupuestos)
    {
        var presupuestoDb = existente.Presupuestos
            .FirstOrDefault(x => x.Id == presupuestoEditado.Id);

        if (presupuestoDb == null)
            continue;

        var realAnterior = presupuestoDb.Real;
        var realNuevo = presupuestoEditado.Real;
        var diferenciaReal = realNuevo - realAnterior;

        presupuestoDb.Presupuesto = presupuestoEditado.Presupuesto;
        presupuestoDb.Real = realNuevo;

        if (diferenciaReal == 0)
            continue;

        // -------------------------
        // SINCRONIZAR AHORROS
        // -------------------------
        if (presupuestoDb.Categoria.EsAhorro || presupuestoDb.Categoria.Tipo == "Ahorro")
        {
            var ahorro = await _db.Ahorros
                .FirstOrDefaultAsync(x => x.CategoriaId == presupuestoDb.CategoriaId);

            if (ahorro != null)
            {
                ahorro.ValorActual += diferenciaReal;

                if (ahorro.ValorActual < 0)
                    ahorro.ValorActual = 0;
            }
        }

        // -------------------------
        // SINCRONIZAR INVERSIONES
        // -------------------------
        if (presupuestoDb.Categoria.EsInversion || presupuestoDb.Categoria.Tipo == "Inversion")
        {
            var inversion = await _db.Inversiones
                .FirstOrDefaultAsync(x => x.CategoriaId == presupuestoDb.CategoriaId);

            if (inversion != null)
            {
                if (inversion.CapitalInicial == 0 && inversion.ValorActual == 0 && diferenciaReal > 0)
                {
                    inversion.CapitalInicial = diferenciaReal;
                    inversion.FechaInicio = DateTime.Today;
                }

                inversion.ValorActual += diferenciaReal;

                if (inversion.ValorActual < 0)
                    inversion.ValorActual = 0;
            }
        }
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

public async Task InicializarCategoriasAsync()
{
    if (await _db.Categorias.AnyAsync())
        return;

    var ahorro = new Categoria { Nombre = "Ahorro e inversion", Orden = 5 };

    _db.Categorias.Add(ahorro);

    await _db.SaveChangesAsync();

    _db.Categorias.AddRange(

    );

    await _db.SaveChangesAsync();
}

// -------------------------
// AHORROS
// -------------------------

public async Task<List<AhorroMeta>> ObtenerAhorrosAsync()
{
    return await _db.Ahorros
        .OrderBy(x => x.Nombre)
        .ToListAsync();
}

public async Task GuardarAhorroAsync(AhorroMeta ahorro)
{
    if (ahorro.Id == 0)
        _db.Ahorros.Add(ahorro);
    else
        _db.Ahorros.Update(ahorro);

    await _db.SaveChangesAsync();
}

public decimal CalcularPorcentajeMetaAhorro(AhorroMeta ahorro)
{
    if (ahorro.MetaObjetivo <= 0) return 0;

    return Math.Round((ahorro.ValorActual / ahorro.MetaObjetivo) * 100, 2);
}


// -------------------------
// INVERSIONES
// -------------------------

public async Task<List<Inversion>> ObtenerInversionesAsync()
{
    return await _db.Inversiones
        .OrderBy(x => x.Tipo)
        .ThenBy(x => x.Nombre)
        .ToListAsync();
}

public async Task GuardarInversionAsync(Inversion inversion)
{
    if (inversion.Id == 0)
        _db.Inversiones.Add(inversion);
    else
        _db.Inversiones.Update(inversion);

    await _db.SaveChangesAsync();
}

public decimal CalcularRendimientoEstimadoMensual(Inversion inversion)
{
    if (inversion.TasaEA <= 0) return 0;

    var tasaMensual =
        (decimal)(Math.Pow(1 + (double)(inversion.TasaEA / 100), 1.0 / 12.0) - 1);

    return decimal.Round(inversion.ValorActual * tasaMensual, 0);
}

public decimal CalcularValorProyectado12Meses(Inversion inversion)
{
    if (inversion.TasaEA <= 0)
        return inversion.ValorActual;

    var valor =
        inversion.ValorActual *
        (decimal)Math.Pow(1 + (double)(inversion.TasaEA / 100), 1.0);

    return decimal.Round(valor, 0);
}


// -------------------------
// RESUMEN
// -------------------------

public async Task<decimal> ObtenerTotalAhorrosAsync()
{
    return (await _db.Ahorros.ToListAsync())
        .Sum(x => x.ValorActual);
}

public async Task<decimal> ObtenerTotalInversionesAsync()
{
    return (await _db.Inversiones.ToListAsync())
        .Sum(x => x.ValorActual);
}
public decimal CalcularValorEstimado(Inversion inv)
{
    if (inv.TasaEA <= 0)
        return inv.CapitalInicial;

    var dias = (DateTime.Today - inv.FechaInicio).TotalDays;

    var tasaDiaria = Math.Pow(1 + (double)(inv.TasaEA / 100), 1.0 / 365) - 1;

    var valor = inv.CapitalInicial * (decimal)Math.Pow(1 + tasaDiaria, dias);

    return decimal.Round(valor, 0);
}


public decimal CalcularDiferenciaRealVsEstimado(Inversion inv)
{
    var estimado = CalcularValorEstimado(inv);

    return inv.ValorActual - estimado;
}
public async Task EliminarInversionAsync(int id)
{
    var inv = await _db.Inversiones.FindAsync(id);

    if (inv != null)
    {
        _db.Inversiones.Remove(inv);
        await _db.SaveChangesAsync();
    }
}

public async Task EliminarAhorroAsync(int id)
{
    var ahorro = await _db.Ahorros.FindAsync(id);

    if (ahorro != null)
    {
        _db.Ahorros.Remove(ahorro);
        await _db.SaveChangesAsync();
    }
}

}