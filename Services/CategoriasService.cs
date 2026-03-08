using FinanzasApp.Data;
using FinanzasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzasApp.Services;

public class CategoriasService
{
    private readonly FinanzasDbContext _db;

    public CategoriasService(FinanzasDbContext db)
    {
        _db = db;
    }

    public async Task<List<Categoria>> ObtenerCategoriasAsync()
    {
        return await _db.Categorias
            .AsNoTracking()
            .Include(x => x.SubCategorias)
            .Where(x => x.CategoriaPadreId == null)
            .OrderBy(x => x.Orden)
            .ToListAsync();
    }

    public async Task<Categoria?> ObtenerPorIdAsync(int id)
    {
        return await _db.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

public async Task GuardarCategoriaAsync(Categoria categoria)
{
    // Determinar tipo automáticamente
    categoria.EsAhorro = categoria.Tipo == "Ahorro";
    categoria.EsInversion = categoria.Tipo == "Inversion";

    // Siempre sincronizar nombre del activo
    categoria.ActivoNombre = categoria.Nombre;

    Categoria? existente = null;

    if (categoria.Id == 0)
    {
        _db.Categorias.Add(categoria);
        await _db.SaveChangesAsync();
        existente = categoria;
    }
    else
    {
        existente = await _db.Categorias
            .FirstOrDefaultAsync(x => x.Id == categoria.Id);

        if (existente == null)
            return;

        existente.Nombre = categoria.Nombre;
        existente.CategoriaPadreId = categoria.CategoriaPadreId;
        existente.Orden = categoria.Orden;
        existente.PorcentajeObjetivo = categoria.PorcentajeObjetivo;
        existente.Tipo = categoria.Tipo;
        existente.EsAhorro = categoria.EsAhorro;
        existente.EsInversion = categoria.EsInversion;
        existente.ActivoNombre = categoria.Nombre;

        await _db.SaveChangesAsync();
    }

    var cat = existente!;

    // -------------------------
    // SINCRONIZAR AHORROS
    // -------------------------

    var ahorro = await _db.Ahorros
        .FirstOrDefaultAsync(x => x.CategoriaId == cat.Id);

    if (cat.EsAhorro)
    {
        if (ahorro == null)
        {
            _db.Ahorros.Add(new AhorroMeta
            {
                CategoriaId = cat.Id,
                Nombre = cat.Nombre,
                Tipo = "Ahorro",
                ValorActual = 0,
                MetaObjetivo = 0
            });
        }
        else
        {
            ahorro.Nombre = cat.Nombre;
            ahorro.Tipo = "Ahorro";
        }
    }
    else
    {
        if (ahorro != null)
            _db.Ahorros.Remove(ahorro);
    }

    // -------------------------
    // SINCRONIZAR INVERSIONES
    // -------------------------

    var inversion = await _db.Inversiones
        .FirstOrDefaultAsync(x => x.CategoriaId == cat.Id);

    if (cat.EsInversion)
    {
        if (inversion == null)
        {
            _db.Inversiones.Add(new Inversion
            {
                CategoriaId = cat.Id,
                Nombre = cat.Nombre,
                Tipo = "Inversion",
                CapitalInicial = 0,
                ValorActual = 0,
                TasaEA = 0,
                FechaInicio = DateTime.Today
            });
        }
        else
        {
            inversion.Nombre = cat.Nombre;
            inversion.Tipo = "Inversion";
        }
    }
    else
    {
        if (inversion != null)
            _db.Inversiones.Remove(inversion);
    }

    await _db.SaveChangesAsync();
}

    public async Task EliminarCategoriaAsync(int id)
    {
        var categoria = await _db.Categorias
            .Include(x => x.SubCategorias)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == null)
            return;

        var ahorro = await _db.Ahorros
            .FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (ahorro != null)
            _db.Ahorros.Remove(ahorro);

        var inversion = await _db.Inversiones
            .FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (inversion != null)
            _db.Inversiones.Remove(inversion);

        if (categoria.SubCategorias.Any())
        {
            foreach (var sub in categoria.SubCategorias)
            {
                var ahorroSub = await _db.Ahorros
                    .FirstOrDefaultAsync(x => x.CategoriaId == sub.Id);

                if (ahorroSub != null)
                    _db.Ahorros.Remove(ahorroSub);

                var inversionSub = await _db.Inversiones
                    .FirstOrDefaultAsync(x => x.CategoriaId == sub.Id);

                if (inversionSub != null)
                    _db.Inversiones.Remove(inversionSub);
            }

            _db.Categorias.RemoveRange(categoria.SubCategorias);
        }

        _db.Categorias.Remove(categoria);

        await _db.SaveChangesAsync();
    }
}