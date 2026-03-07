namespace FinanzasApp.Models;

public class FondoEmpleado
{
    public int Id { get; set; }

    public string Entidad { get; set; } = "";

    public decimal ValorActual { get; set; }

    public decimal AporteMensual { get; set; }
}