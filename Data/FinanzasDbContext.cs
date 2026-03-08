using FinanzasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzasApp.Data;

public class FinanzasDbContext : DbContext
{
    public FinanzasDbContext(DbContextOptions<FinanzasDbContext> options)
        : base(options)
    {
    }

    public DbSet<ConfiguracionFinanciera> Configuraciones => Set<ConfiguracionFinanciera>();
    public DbSet<MesFinanciero> MesesFinancieros { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<PresupuestoCategoria> Presupuestos { get; set; }

    public DbSet<Deuda> Deudas { get; set; }

    public DbSet<Inversion> Inversiones { get; set; }

    public DbSet<AhorroMeta> Ahorros { get; set; }

    public DbSet<FondoEmpleado> FondosEmpleado { get; set; }

    public DbSet<Accion> Acciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PresupuestoCategoria>()
            .HasOne(x => x.Mes)
            .WithMany(x => x.Presupuestos)
            .HasForeignKey(x => x.MesFinancieroId);

        modelBuilder.Entity<PresupuestoCategoria>()
            .HasOne(x => x.Categoria)
            .WithMany()
            .HasForeignKey(x => x.CategoriaId);

        modelBuilder.Entity<Categoria>()
            .HasOne(x => x.CategoriaPadre)
            .WithMany(x => x.SubCategorias)
            .HasForeignKey(x => x.CategoriaPadreId)
            .OnDelete(DeleteBehavior.Restrict);
            

    
        modelBuilder.Entity<Categoria>().HasData(

            new Categoria { Id = 1, Nombre = "Casa" },
            new Categoria { Id = 2, Nombre = "Arriendo", CategoriaPadreId = 1 },
            new Categoria { Id = 3, Nombre = "Mercado", CategoriaPadreId = 1 },
            new Categoria { Id = 4, Nombre = "Servicios", CategoriaPadreId = 1 },

            new Categoria { Id = 5, Nombre = "Transporte" },
            new Categoria { Id = 6, Nombre = "Gasolina", CategoriaPadreId = 5 },
            new Categoria { Id = 7, Nombre = "Uber", CategoriaPadreId = 5 },

            new Categoria { Id = 8, Nombre = "Deudas" },
            new Categoria { Id = 9, Nombre = "Tarjeta Visa", CategoriaPadreId = 8 },
            new Categoria { Id = 10, Nombre = "Credito carro", CategoriaPadreId = 8 }


            );
        modelBuilder.Entity<MesFinanciero>()
            .HasIndex(x => x.MesKey)
            .IsUnique();

        modelBuilder.Entity<ConfiguracionFinanciera>()
            .HasData(new ConfiguracionFinanciera
            {
                Id = 1,
                Moneda = "COP",
                IngresoBaseMensual = 0,
                PorcCasaFijosMin = 23,
                PorcCasaFijosMax = 28,
                PorcTransporteMin = 4,
                PorcTransporteMax = 5,
                PorcDeudaMin = 18,
                PorcDeudaMax = 22,
                PorcAhorroInversion = 7,
                PorcDiversionMax = 3,
                PorcOtrosMax = 8,
                UmbralAmarillo = 95,
                UmbralRojo = 105
            });
    }
}