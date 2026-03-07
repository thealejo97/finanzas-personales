namespace FinanzasApp.Models;

public class ConfiguracionFinanciera
{
    public int Id { get; set; }
    public string Moneda { get; set; } = "COP";
    public decimal IngresoBaseMensual { get; set; }

    public decimal PorcCasaFijosMin { get; set; }
    public decimal PorcCasaFijosMax { get; set; }

    public decimal PorcTransporteMin { get; set; }
    public decimal PorcTransporteMax { get; set; }

    public decimal PorcDeudaMin { get; set; }
    public decimal PorcDeudaMax { get; set; }

    public decimal PorcAhorroInversion { get; set; }
    public decimal PorcDiversionMax { get; set; }
    public decimal PorcOtrosMax { get; set; }

    public decimal UmbralAmarillo { get; set; }
    public decimal UmbralRojo { get; set; }
}