using QuizzApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Ajoute la gestion des sessions (si tu prévois d'utiliser des sessions pour stocker des données)
builder.Services.AddSession();

// Enregistre le service pour lire les questions à partir du JSON
builder.Services.AddSingleton<QuestionService>(); // Assurez-vous d'avoir importé le namespace approprié

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Active la gestion des sessions
app.UseSession();

app.UseAuthorization();

// Configure les routes pour les contrôleurs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Questions}/{action=Index}/{id?}"); // Change le contrôleur par défaut si nécessaire

app.Run();
