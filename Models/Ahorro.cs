namespace FinanzasApp.Models;

public class Ahorro
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    // Cajita, Alto rendimiento, Emergencia, etc.
    public string Tipo { get; set; } = "";

    public decimal ValorActual { get; set; }

    public decimal MetaObjetivo { get; set; }
}