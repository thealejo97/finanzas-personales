namespace FinanzasApp.Models;

public class PresupuestoCategoria
{
    public int Id { get; set; }

    public int MesFinancieroId { get; set; }

    public MesFinanciero Mes { get; set; }

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }

    public decimal Presupuesto { get; set; }

    public decimal Real { get; set; }
}