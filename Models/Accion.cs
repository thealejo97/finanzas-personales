namespace FinanzasApp.Models;

public class Accion
{
    public int Id { get; set; }

    public string Empresa { get; set; } = "";

    public decimal Cantidad { get; set; }

    public decimal PrecioPromedio { get; set; }

    public decimal PrecioActual { get; set; }

    public decimal ValorActual =>
        Cantidad * PrecioActual;
}