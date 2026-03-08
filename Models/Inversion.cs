namespace FinanzasApp.Models;

public class Inversion
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = "";

    public string Tipo { get; set; } = "";

    public decimal CapitalInicial { get; set; }

    public decimal ValorActual { get; set; }

    public decimal TasaEA { get; set; }

    public DateTime FechaInicio { get; set; }
}