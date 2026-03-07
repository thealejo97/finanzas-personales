namespace FinanzasApp.Models;

public class Inversion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    // CDT / FIC / Accion
    public string Tipo { get; set; } = "";

    public decimal CapitalInicial { get; set; }

    public decimal TasaEA { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.Today;

    public decimal AportesAcumulados { get; set; }

    public decimal ValorActual
    {
        get
        {
            var baseCapital = CapitalInicial + AportesAcumulados;

            if (baseCapital <= 0 || TasaEA <= 0)
                return baseCapital;

            var dias = Math.Max((DateTime.Today - FechaInicio).Days, 0);

            var tasaDiaria = (decimal)(Math.Pow(1 + (double)(TasaEA / 100), 1.0 / 365.0) - 1);

            var valor = baseCapital * (decimal)Math.Pow((double)(1 + tasaDiaria), dias);

            return decimal.Round(valor, 0);
        }
    }
}