using FinanzasApp.Components;
using FinanzasApp.Data;
using FinanzasApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<FinanzasDbContext>(options =>
    options.UseSqlite("Data Source=Database/finanzas.db"));

builder.Services.AddScoped<FinanzasService>();
builder.Services.AddScoped<CategoriasService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FinanzasDbContext>();
    db.Database.Migrate();

    var finanzasService = scope.ServiceProvider.GetRequiredService<FinanzasService>();
    await finanzasService.InicializarDatosBaseAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();