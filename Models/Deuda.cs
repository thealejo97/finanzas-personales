namespace FinanzasApp.Models;

public class Deuda
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public decimal SaldoActual { get; set; }

    public string TipoTasa { get; set; } = "EA";

    public decimal Tasa { get; set; }

    public decimal PagoMinimo { get; set; }

    public decimal PagoObjetivo { get; set; }

    public int PrioridadManual { get; set; }
}