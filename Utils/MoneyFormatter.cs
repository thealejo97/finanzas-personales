using System.Globalization;

namespace FinanzasApp.Utils;

public static class MoneyFormatter
{
    private static CultureInfo culture = new CultureInfo("en-US");

    public static string Formatear(decimal valor)
    {
        return valor.ToString("#,0.##", culture);
    }

    public static decimal Parsear(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return 0;

        texto = texto.Replace(",", "");

        if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out var valor))
            return valor;

        return 0;
    }
}