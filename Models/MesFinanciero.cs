namespace FinanzasApp.Models;

public class MesFinanciero
{
    public int Id { get; set; }

    public string MesKey { get; set; } = "";

    public decimal Ingreso { get; set; }

    public bool EstaCerrado { get; set; }

    public DateTime? FechaCierre { get; set; }

    public List<PresupuestoCategoria> Presupuestos { get; set; } = new();

    // cálculos automáticos

    public decimal TotalReal =>
        Presupuestos.Sum(x => x.Real);

    public decimal TotalPresupuesto =>
        Presupuestos.Sum(x => x.Presupuesto);

    public decimal ResultadoMes =>
        Ingreso - TotalReal;

    public decimal TasaAhorro =>
        Ingreso == 0 ? 0 :
        (Presupuestos
            .Where(x => x.Categoria.Nombre.Contains("Ahorro"))
            .Sum(x => x.Real) / Ingreso) * 100;
}