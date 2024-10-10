using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services nécessaires
builder.Services.AddControllers(); // Permet d'ajouter des contrôleurs

var app = builder.Build();

// Configurer l'application pour servir les fichiers statiques
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles(); // Pour servir les fichiers statiques (HTML, CSS, JS)

app.UseRouting();

app.UseAuthorization();

// Définir les points de terminaison pour les contrôleurs
app.MapControllers();

app.Run();
