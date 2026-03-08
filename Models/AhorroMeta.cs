namespace FinanzasApp.Models;
public class AhorroMeta
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; } = null!;

    public string Nombre { get; set; } = "";

    public string Tipo { get; set; } = "";

    public decimal ValorActual { get; set; }

    public decimal MetaObjetivo { get; set; }
}