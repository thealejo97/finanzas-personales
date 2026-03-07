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
            .Include(x => x.SubCategorias.OrderBy(s => s.Orden))
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
        if (categoria.Id == 0)
        {
            _db.Categorias.Add(categoria);
            await _db.SaveChangesAsync();
            return;
        }

        var existente = await _db.Categorias
            .FirstOrDefaultAsync(x => x.Id == categoria.Id);

        if (existente == null)
            return;

        existente.Nombre = categoria.Nombre;
        existente.Orden = categoria.Orden;
        existente.PorcentajeObjetivo = categoria.PorcentajeObjetivo;
        existente.Tipo = categoria.Tipo;
        existente.CategoriaPadreId = categoria.CategoriaPadreId;

        await _db.SaveChangesAsync();
    }

    public async Task EliminarCategoriaAsync(int id)
    {
        var categoria = await _db.Categorias
            .Include(x => x.SubCategorias)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == null)
            return;

        if (categoria.SubCategorias.Any())
        {
            _db.Categorias.RemoveRange(categoria.SubCategorias);
        }

        _db.Categorias.Remove(categoria);

        await _db.SaveChangesAsync();
    }
}