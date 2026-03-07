namespace FinanzasApp.Models;

public class Inversion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public decimal CapitalInicial { get; set; }

    public decimal AportesAcumulados { get; set; }

    public decimal TasaEA { get; set; }

    public DateTime FechaInicio { get; set; }

    public decimal ValorActual =>
        CapitalInicial + AportesAcumulados;
}