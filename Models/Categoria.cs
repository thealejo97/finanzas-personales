public class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public int? CategoriaPadreId { get; set; }

    public Categoria? CategoriaPadre { get; set; }

    public List<Categoria> SubCategorias { get; set; } = new();

    public int Orden { get; set; }

    public decimal PorcentajeObjetivo { get; set; }

    public string Tipo { get; set; } = "Gasto";

    public bool EsAhorro { get; set; }

    public bool EsInversion { get; set; }

    public string? ActivoNombre { get; set; }
}