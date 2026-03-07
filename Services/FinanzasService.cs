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

    public async Task InicializarCategoriasAsync()
{
    if (await _db.Categorias.AnyAsync())
        return;

    // Categorías padre
    var ingreso = new Categoria { Nombre = "Ingreso", Orden = 1 };
    var casa = new Categoria { Nombre = "Casa", Orden = 2 };
    var transporte = new Categoria { Nombre = "Transporte", Orden = 3 };
    var deuda = new Categoria { Nombre = "Deuda", Orden = 4 };
    var ahorro = new Categoria { Nombre = "Ahorro e inversion", Orden = 5 };
    var diversion = new Categoria { Nombre = "Diversion", Orden = 6 };
    var restante = new Categoria { Nombre = "Restante", Orden = 7 };

    _db.Categorias.AddRange(
        ingreso,
        casa,
        transporte,
        deuda,
        ahorro,
        diversion,
        restante
    );

    await _db.SaveChangesAsync();

    // Subcategorías
    var subcategorias = new List<Categoria>
    {
        new Categoria { Nombre = "Ingreso sueldo", CategoriaPadreId = ingreso.Id },

        new Categoria { Nombre = "Casa", CategoriaPadreId = casa.Id },
        new Categoria { Nombre = "Regalo mamá", CategoriaPadreId = casa.Id },
        new Categoria { Nombre = "Mercado", CategoriaPadreId = casa.Id },
        new Categoria { Nombre = "Regalo extra", CategoriaPadreId = casa.Id },

        new Categoria { Nombre = "Transporte gasolina", CategoriaPadreId = transporte.Id },
        new Categoria { Nombre = "Uber", CategoriaPadreId = transporte.Id },
        new Categoria { Nombre = "Fondo mantenimiento carro", CategoriaPadreId = transporte.Id },

        new Categoria { Nombre = "Carro", CategoriaPadreId = deuda.Id },
        new Categoria { Nombre = "NU tarjeta", CategoriaPadreId = deuda.Id },
        new Categoria { Nombre = "Rappi", CategoriaPadreId = deuda.Id },

        new Categoria { Nombre = "Inversiones (FIC Tyba)", CategoriaPadreId = ahorro.Id },
        new Categoria { Nombre = "Urgencias", CategoriaPadreId = ahorro.Id },
        new Categoria { Nombre = "Urgencias mama", CategoriaPadreId = ahorro.Id },

        new Categoria { Nombre = "Diversion", CategoriaPadreId = diversion.Id },

        new Categoria { Nombre = "Hidra Andrea 1ero", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Pricesmart mamá", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Andrea Medicina Prepa 1ero", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Adriana Medicina Prepa", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Sillón Andrea (5 mes)", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Devolución arriendo", CategoriaPadreId = ingreso.Id },
        new Categoria { Nombre = "Sillón mama (5 cuota)", CategoriaPadreId = ingreso.Id }
    };

    _db.Categorias.AddRange(subcategorias);

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
}