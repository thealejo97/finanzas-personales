namespace FinanzasApp.Models;

public class DashboardResumen
{
    public decimal PatrimonioEstimado { get; set; }
    public decimal DeudaTotal { get; set; }
    public decimal InversionesTotal { get; set; }
    public decimal AhorroTotal { get; set; }
    public decimal FondoEmpleadoTotal { get; set; }
    public decimal AccionesTotal { get; set; }
    public decimal ResultadoMes { get; set; }
    public decimal TasaAhorro { get; set; }
    public decimal RatioDeudaIngreso { get; set; }

    public List<string> Alertas { get; set; } = new();
}