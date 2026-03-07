using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanzasApp.Data;

public class FinanzasDbContextFactory : IDesignTimeDbContextFactory<FinanzasDbContext>
{
    public FinanzasDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanzasDbContext>();

        optionsBuilder.UseSqlite("Data Source=Database/finanzas.db");

        return new FinanzasDbContext(optionsBuilder.Options);
    }
}