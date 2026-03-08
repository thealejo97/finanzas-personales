namespace FinanzasApp.Models;

public class ActivoFinanciero
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; } = null!;

    public decimal Saldo { get; set; }

    public decimal TasaEA { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.Today;

    public string Tipo { get; set; } = "Ahorro";
}