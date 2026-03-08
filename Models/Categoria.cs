namespace FinanzasApp.Models;

public class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public int Orden { get; set; }

    // jerarquía
    public int? CategoriaPadreId { get; set; }

    public Categoria? CategoriaPadre { get; set; }

    public List<Categoria> SubCategorias { get; set; } = new();


    // configuración financiera
    public decimal PorcentajeObjetivo { get; set; }


    // tipo de categoría
    public string Tipo { get; set; } = "Gasto";
    // valores posibles:
    // Gasto
    // Ingreso
    // Ahorro
    // Inversion


    // sincronización con patrimonio
    public bool EsAhorro { get; set; }

    public bool EsInversion { get; set; }

    public string? ActivoNombre { get; set; }
}